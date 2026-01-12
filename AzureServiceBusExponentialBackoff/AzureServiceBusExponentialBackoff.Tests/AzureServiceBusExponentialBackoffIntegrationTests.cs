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

        // while (true)
        // {
        //     var m = await receiver.ReceiveMessageAsync();
        //     await receiver.CompleteMessageAsync(m);
        //     // var receiveDeferredMessageAsync = await receiver.ReceiveDeferredMessageAsync(194);
        //     // await receiver.CompleteMessageAsync(receiveDeferredMessageAsync);
        // }

        await sender.SendMessageAsync(new ServiceBusMessage("Hello World!"));
        var message = await receiver.ReceiveMessageAsync();

        var reference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(message.SequenceNumber)))
        {
            MessageId = $"{message.MessageId}_ref_0",
            ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(2)
        };

        await sender.SendMessageAsync(reference);
        await receiver.AbandonMessageAsync(message);

        var originalMessageAgain = await receiver.ReceiveMessageAsync();
        var anotherReference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(originalMessageAgain.SequenceNumber)))
        {
            MessageId = $"{message.MessageId}_ref_1",
            ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(2)
        };
        await sender.SendMessageAsync(anotherReference);
        await receiver.DeferMessageAsync(originalMessageAgain);

        var counter = new Counter();
        await Task.WhenAll(ProcessMessage(1, counter), ProcessMessage(2, counter));
        counter.Value.ShouldBe(1, "The original message should be processed only once");
    }

    private async Task ProcessMessage(int threadIndex, Counter counter)
    {
        var client = new ServiceBusClient(ServiceBusConnectionString);
        var receiver = client.CreateReceiver(QueueName);
        try
        {
            var refMessage = await receiver.ReceiveMessageAsync();
            var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
            Console.WriteLine($"Thread {threadIndex} received ref message with body: {refMessage.Body}");
            var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber);
            Console.WriteLine($"Thread {threadIndex} started processing deferred message");
            counter.Increment();
            Thread.Sleep(5000);
            Console.WriteLine($"Thread {threadIndex} finished processing deferred message");
            await receiver.CompleteMessageAsync(deferredMessage);
            await receiver.CompleteMessageAsync(refMessage);
        }
        catch
        {
            // ignored
        }
    }
}
