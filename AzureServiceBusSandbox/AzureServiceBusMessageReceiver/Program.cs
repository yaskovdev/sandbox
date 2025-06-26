namespace AzureServiceBusMessageReceiver;

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
        await using var processor = client.CreateProcessor(QueueName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1,
            MaxAutoLockRenewalDuration = Timeout.InfiniteTimeSpan
        });

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();
        Console.WriteLine("Receiving messages. Press any key to exit...");
        Console.ReadKey();
        await processor.StopProcessingAsync();
    }

    private static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body}");
        // Simulate processing (lock will be auto-renewed)
        await Task.Delay(TimeSpan.FromSeconds(300));
        await args.CompleteMessageAsync(args.Message);
        Console.WriteLine("Message processed and completed");
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Error: {args.Exception.Message}");
        await Task.CompletedTask;
    }
}
