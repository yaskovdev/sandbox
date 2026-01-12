namespace AzureServiceBusExponentialBackoff.Tests;

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
public class AzureServiceBusExponentialBackoffIntegrationTests
{
    private const string ServiceBusConnectionString = "";
    private const string QueueName = "sandbox_queue";

    [TestMethod]
    public async Task ShouldHandleMessageOnce_WhenThereAreTwoReferences()
    {
        var client = new ServiceBusClient(ServiceBusConnectionString);
        var sender = client.CreateSender(QueueName);
        var receiver = client.CreateReceiver(QueueName);

        await sender.SendMessageAsync(new ServiceBusMessage("Hello World!"));

        // Send reference message but fail to defer the original message
        var message = await receiver.ReceiveMessageAsync();
        var reference = CreateReferenceMessage(message, 0);
        await sender.SendMessageAsync(reference);
        await receiver.AbandonMessageAsync(message);

        // Send reference message and defer the original message
        var originalMessageAgain = await receiver.ReceiveMessageAsync();
        var anotherReference = CreateReferenceMessage(originalMessageAgain, 1);
        await sender.SendMessageAsync(anotherReference);
        await receiver.DeferMessageAsync(originalMessageAgain);

        var counter = new Counter();
        await Task.WhenAll(ProcessMessage(0, counter), ProcessMessage(1, counter));
        counter.Value.ShouldBe(1, "The original message should be processed only once");
    }

    private static ServiceBusMessage CreateReferenceMessage(ServiceBusReceivedMessage message, int refIndex) =>
        new(JsonSerializer.Serialize(new MessageReference(message.SequenceNumber)))
        {
            MessageId = $"{message.MessageId}_ref_{refIndex}"
        };

    private static async Task ProcessMessage(int threadIndex, Counter counter)
    {
        var client = new ServiceBusClient(ServiceBusConnectionString);
        var receiver = client.CreateReceiver(QueueName);
        var refMessage = await receiver.ReceiveMessageAsync();
        try
        {
            var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
            Console.WriteLine($"Thread {threadIndex} received ref message with body: {refMessage.Body}");
            var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber);
            Console.WriteLine($"Thread {threadIndex} started processing deferred message");
            counter.Increment();
            Console.WriteLine($"Thread {threadIndex} finished processing deferred message");
            await receiver.CompleteMessageAsync(deferredMessage);
            await receiver.CompleteMessageAsync(refMessage);
        }
        catch (ServiceBusException e) when (e.Reason == ServiceBusFailureReason.MessageNotFound)
        {
            Console.WriteLine($"The original deferred message was not found: {e.Message}. Most likely it expired, got processed, or wasn't deferred. Completing the ref message to stop further retries");
            await receiver.CompleteMessageAsync(refMessage);
        }
        catch
        {
            // ignored
        }
    }
}
