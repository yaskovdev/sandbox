namespace LoadBalancer;

/// <summary>
/// Ensures immediate instantiation of a service during application startup, without waiting for the first request.
/// </summary>
public class EagerInstantiationHostedService<T>(T service) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
}
