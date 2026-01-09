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
        Console.WriteLine("Sending message with ID " + MessageId);

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
        Console.WriteLine("Processing reference message with ID " + refMessage.MessageId + " and body " + refMessage.Body);
        var body = JsonSerializer.Deserialize<MessageReference>(refMessage.Body);
        var deferredMessage = await receiver.ReceiveDeferredMessageAsync(body.ReferencedMessageSequenceNumber); // TODO: what if the original message is not found?
        Console.WriteLine("Retrieved deferred message with ID " + deferredMessage.MessageId + ", delivery count " + deferredMessage.DeliveryCount + " and body " + deferredMessage.Body);
        await ProcessMessage(sender, receiver, deferredMessage);
        await receiver.CompleteMessageAsync(refMessage); // TODO: isn't it too early to complete it here?
    }

    private static async Task ProcessMessage(ServiceBusSender sender, ServiceBusReceiver receiver, ServiceBusReceivedMessage message)
    {
        try
        {
            var applicationProperties = message.ApplicationProperties;
            Console.WriteLine("Processing message with ID " + message.MessageId + ", body " + message.Body + " and properties " + string.Join(", ", applicationProperties.Select(kvp => kvp.Key + "=" + kvp.Value)));
            var body = JsonSerializer.Deserialize<MessageBody>(message.Body);
            if (message.DeliveryCount < 4)
            {
                throw new Exception("Simulated processing failure");
            }
            Console.WriteLine("Successfully processed message with payload: " + body.Content);
            await receiver.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message + ". Deferring the original message and sending a reference to it");
            var reference = new ServiceBusMessage(JsonSerializer.Serialize(new MessageReference(message.SequenceNumber)))
            {
                // Schedule the message with exponential backoff: 2^n seconds
                MessageId = message.MessageId + "_ref_" + message.DeliveryCount,
                ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(Math.Pow(2, message.DeliveryCount))
            };

            // The order is important: only after the reference message is successfully sent, we defer the original message
            await sender.SendMessageAsync(reference);
            await receiver.DeferMessageAsync(message);
        }
    }
}
