using Microsoft.Extensions.Hosting;

namespace MediaApp.Bootstrap;

public class WorkerService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("WorkerService started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping WorkerService, it may take long time...");
        Thread.Sleep(7000);
        Console.WriteLine("WorkerService stopped");
        return Task.CompletedTask;
    }
}
