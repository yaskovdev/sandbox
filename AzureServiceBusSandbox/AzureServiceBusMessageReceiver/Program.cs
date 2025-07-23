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
            MaxConcurrentCalls = 2,
            MaxAutoLockRenewalDuration = TimeSpan.FromSeconds(10)
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
        // await Task.Delay(TimeSpan.FromSeconds(300));

        // Simulate a deadlock scenario to check if the message lock is auto-renewed
        Deadlock();

        await args.CompleteMessageAsync(args.Message);
        Console.WriteLine("Message processed and completed");
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Error: {args.Exception.Message}");
        await Task.CompletedTask;
    }

    private static void Deadlock()
    {
        Console.WriteLine("Entering a deadlock scenario...");
        var lock1 = new object();
        var lock2 = new object();

        var t1 = new Thread(() =>
        {
            Console.WriteLine("Thread 1 starting...");
            lock (lock1)
            {
                Console.WriteLine("Thread 1 acquired lock 1, waiting to acquire lock 2...");
                Thread.Sleep(100); // Ensure thread 2 has time to acquire lock2

                lock (lock2)
                {
                    // This will never execute
                    Console.WriteLine("Thread 1 acquired both locks");
                }
            }
        });

        var t2 = new Thread(() =>
        {
            Console.WriteLine("Thread 2 starting...");
            lock (lock2)
            {
                Console.WriteLine("Thread 2 acquired lock 2, waiting to acquire lock 1...");
                Thread.Sleep(100); // Ensure thread 1 has time to acquire lock1

                lock (lock1)
                {
                    // This will never execute
                    Console.WriteLine("Thread 2 acquired both locks");
                }
            }
        });

        t1.Start();
        t2.Start();

        // Wait for threads to finish (they won't)
        t1.Join();
        t2.Join();
        Console.WriteLine("Exiting a deadlock scenario (that should not happen, it's a deadlock, fater all)");
    }
}
