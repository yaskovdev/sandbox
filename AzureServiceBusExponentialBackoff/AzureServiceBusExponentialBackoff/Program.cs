// See https://aka.ms/new-console-template for more information

namespace AzureServiceBusExponentialBackoff;

using System.Collections.Immutable;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

internal static class Program
{
    private const string MessageId = "163";

    public static async Task Main(string[] args)
    {
        var client = new ServiceBusClient("Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;");
        var sender = client.CreateSender("post_processing_queue");
        Console.WriteLine($"Sending message with ID {MessageId}");

        var messageJson = JsonSerializer.Serialize(new MessageBody(1, "Hello World!"));
        await sender.SendMessageAsync(new ServiceBusMessage(messageJson)
        {
            MessageId = MessageId
        });

        var t = new Thread(async () =>
        {
            Console.WriteLine("Starting receiver");
            var receiver = client.CreateReceiver("post_processing_queue");
            while (true)
            {
                var message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(2));
                if (message is null)
                {
                    Console.WriteLine("Waiting for messages...");
                }
                else if (message.MessageId.Contains("_ref_"))
                {
                    await ProcessRefMessage(sender, receiver, message);
                }
                else
                {
                    await ProcessMessage(sender, receiver, message);
                }
            }
        });

        t.Start();
        Thread.Sleep(Timeout.Infinite);
        t.Join();
    }

    private static async Task ProcessRefMessage(ServiceBusSender sender, ServiceBusReceiver receiver, ServiceBusReceivedMessage refMessage)
    {
        try
        {
            Console.WriteLine($"Processing reference message with ID {refMessage.MessageId} and body {refMessage.Body}");
            var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
            var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber);
            Console.WriteLine($"Retrieved deferred message with ID {deferredMessage.MessageId}, delivery count {deferredMessage.DeliveryCount} and body {deferredMessage.Body}");
            await ProcessMessage(sender, receiver, deferredMessage);
            await receiver.CompleteMessageAsync(refMessage);
        }
        catch (ServiceBusException e) when (e.Reason == ServiceBusFailureReason.MessageNotFound)
        {
            Console.WriteLine($"The original deferred message was not found: {e.Message}. Most likely it expired, or the instance sent the ref message, then crashed before deferring the original message. Completing the ref message to stop further retries");
            await receiver.CompleteMessageAsync(refMessage);
        }
        catch (Exception e)
        {
            // ProcessMessage handles its own exceptions, so if we are here, something unexpected happened, and we hope another instance will be more lucky
            Console.WriteLine($"Exception occurred while handling the ref message: {e.Message}. Abandoning the ref message to let another instance to retry");
            await receiver.AbandonMessageAsync(refMessage);
        }
    }

    private static async Task ProcessMessage(ServiceBusSender sender, ServiceBusReceiver receiver, ServiceBusReceivedMessage message)
    {
        try
        {
            Console.WriteLine($"Processing message with ID {message.MessageId}, body {message.Body}");
            var body = JsonSerializer.Deserialize<MessageBody>(message.Body);
            if (message.DeliveryCount < 4)
            {
                throw new Exception("Simulated processing failure");
            }
            Console.WriteLine($"Successfully processed message with payload: {body.Content}");
            await receiver.CompleteMessageAsync(message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occurred: {e.Message}. Deferring the original message and sending a reference to it");
            var reference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(message.SequenceNumber)))
            {
                // Schedule the message with exponential backoff: 2^n seconds
                MessageId = $"{message.MessageId}_ref_{message.DeliveryCount}",
                ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(Math.Pow(2, message.DeliveryCount))
            };

            // The order is important: only after the reference message is successfully sent, we defer the original message.
            // If we send the reference first, and crash-stop before deferring, we will have a reference to a message that is still active in the queue.
            // But it is not an issue, because the original message will be redelivered and processed again, but the reference message will be completed
            // without any action.
            await sender.SendMessageAsync(reference);
            // Once this returns, the message will stay in the queue, but will never be delivered unless we explicitly retrieve it by sequence number
            await receiver.DeferMessageAsync(message);
        }
    }
}
