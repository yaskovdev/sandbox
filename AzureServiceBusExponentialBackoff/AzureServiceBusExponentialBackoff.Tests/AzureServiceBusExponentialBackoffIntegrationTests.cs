namespace AzureServiceBusExponentialBackoff.Tests;

using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        //     // var receiveDeferredMessageAsync = await receiver.ReceiveDeferredMessageAsync(184);
        //     // await receiver.CompleteMessageAsync(receiveDeferredMessageAsync);
        // }

        await sender.SendMessageAsync(new ServiceBusMessage("Hello World!"));
        var message = await receiver.ReceiveMessageAsync();

        var reference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(message.SequenceNumber)))
        {
            MessageId = $"{message.MessageId}_ref_{message.DeliveryCount}",
            ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(5)
        };

        await sender.SendMessageAsync(reference);
        await receiver.AbandonMessageAsync(message);

        var originalMessageAgain = await receiver.ReceiveMessageAsync();
        var anotherReference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(originalMessageAgain.SequenceNumber)))
        {
            MessageId = $"{message.MessageId}_ref_{message.DeliveryCount}",
            ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(5)
        };
        await sender.SendMessageAsync(anotherReference);
        await receiver.DeferMessageAsync(originalMessageAgain);

        var t1 = new Thread(async () =>
        {
            var c = new ServiceBusClient(ServiceBusConnectionString);
            var r = c.CreateReceiver(QueueName);
            var refMessage = await r.ReceiveMessageAsync();
            var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
            Console.WriteLine("Thread 1 received ref message with body: " + refMessage.Body);
            var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber);
            Console.WriteLine("Thread 1 started processing deferred message");
            Thread.Sleep(5000);
            Console.WriteLine("Thread 1 finished processing deferred message");
            await r.CompleteMessageAsync(deferredMessage);
            await r.CompleteMessageAsync(refMessage);
        });
        var t2 = new Thread(async () =>
        {
            var c = new ServiceBusClient(ServiceBusConnectionString);
            var r = c.CreateReceiver(QueueName);
            var refMessage = await r.ReceiveMessageAsync();
            var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
            Console.WriteLine("Thread 2 received ref message with body: " + refMessage.Body);
            var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber);
            Console.WriteLine("Thread 2 started processing deferred message");
            Thread.Sleep(5000);
            Console.WriteLine("Thread 2 finished processing deferred message");
            await r.CompleteMessageAsync(deferredMessage);
            await r.CompleteMessageAsync(refMessage);
        });
        t1.Start();
        t2.Start();
        Thread.Sleep(Timeout.Infinite);
        t1.Join();
        t2.Join();
    }
}
