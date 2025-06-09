namespace MediaApp;

// ReSharper disable once ClassNeverInstantiated.Global, this is instantiated by the factory
public class Session(string id, ILogger<Session> logger) : IDisposable
{
    private int _disposed;

    public void Start()
    {
        ChaosMonkeyPolicies.LatencyFaultPolicy(TimeSpan.FromSeconds(4), 0.2, new InvalidOperationException("Cannot start session, Media Platform is down"), 0.1)
            .Execute(() => logger.LogInformation("Session with ID: {SessionId} started", id));
    }

    public void Dispose()
    {
        ChaosMonkeyPolicies.LatencyPolicy(TimeSpan.FromSeconds(10), 0.3).Execute(() =>
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
            {
                logger.LogInformation("Session with ID: {SessionId} disposed", id);
            }

            GC.SuppressFinalize(this);
        });
    }
}
