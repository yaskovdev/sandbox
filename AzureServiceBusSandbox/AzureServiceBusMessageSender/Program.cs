using Azure.Messaging.ServiceBus;

namespace AzureServiceBusMessageSender;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var connectionString = "<connection-string>";
        var queueName = "session-composition-queue";
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(queueName);

        var uuid = Guid.NewGuid().ToString();
        var message = new ServiceBusMessage(uuid);

        await sender.SendMessageAsync(message);

        Console.WriteLine($"Sent message with UUID: {uuid}");
    }
}