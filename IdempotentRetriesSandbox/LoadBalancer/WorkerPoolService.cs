namespace LoadBalancer;

/// <summary>
/// Eagerly initializes the worker pool at the application startup.
/// </summary>
public class WorkerPoolService(IWorkerPool workerPool) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
}
