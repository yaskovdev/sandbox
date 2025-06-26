namespace AzureServiceBusMessageSender;

using Azure.Messaging.ServiceBus;

internal static class Program
{
    private const string ServiceBusEnvironmentVariable = "AZURE_SERVICE_BUS_CONNECTION_STRING";
    private const string QueueName = "session-composition-queue";

    public static async Task Main(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable(ServiceBusEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine($"Environment variable {ServiceBusEnvironmentVariable} is not set");
            return;
        }

        await using var client = new ServiceBusClient(connectionString);
        await using var sender = client.CreateSender(QueueName);

        var uuid = Guid.NewGuid().ToString();
        var message = new ServiceBusMessage(uuid)
        {
            TimeToLive = TimeSpan.FromSeconds(600)
        };

        await sender.SendMessageAsync(message);

        Console.WriteLine($"Sent message with UUID: {uuid}");
    }
}
