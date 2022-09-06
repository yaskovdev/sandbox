using Microsoft.Extensions.Hosting;

namespace MediaApp.Bootstrap;

public class WorkerService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting WorkerService, it may take long time...");
        Thread.Sleep(5000);
        Console.WriteLine("WorkerService started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("WorkerService stopped");
        return Task.CompletedTask;
    }
}
