namespace AzureServiceBusMessageSender;

using Azure.Messaging.ServiceBus;

internal static class Program
{
    private const string ServiceBusEnvironmentVariable = "AZURE_SERVICE_BUS_CONNECTION_STRING";

    public static async Task Main(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable(ServiceBusEnvironmentVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine($"Environment variable {ServiceBusEnvironmentVariable} is not set");
            return;
        }

        var queueName = "session-composition-queue";
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(queueName);

        var uuid = Guid.NewGuid().ToString();
        var message = new ServiceBusMessage(uuid);

        await sender.SendMessageAsync(message);

        Console.WriteLine($"Sent message with UUID: {uuid}");
    }
}
