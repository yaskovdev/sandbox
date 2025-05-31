namespace MediaApp;

using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;

// ReSharper disable once ClassNeverInstantiated.Global, this is instantiated by the factory
public class Session : IDisposable
{
    private readonly string _id;
    private readonly ILogger<Session> _logger;
    private readonly InjectOutcomePolicy _chaosPolicy;
    private int _disposed;

    public Session(string id, ILogger<Session> logger)
    {
        _id = id;
        _logger = logger;
        _chaosPolicy = MonkeyPolicy.InjectException(with =>
            with.Fault(new InvalidOperationException("Cannot start session, Media Platform is down"))
                .InjectionRate(0.4)
                .Enabled(true)
        );
        Start();
    }

    private void Start()
    {
        _chaosPolicy.Execute(() => _logger.LogInformation("Session with ID: {SessionId} started", _id));
    }

    public void Dispose()
    {
        if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
        {
            _logger.LogInformation("Session with ID: {SessionId} disposed", _id);
        }

        GC.SuppressFinalize(this);
    }
}
