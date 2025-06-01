namespace MediaApp;

using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Latency;
using Polly.Contrib.Simmy.Outcomes;
using Polly.Wrap;

// ReSharper disable once ClassNeverInstantiated.Global, this is instantiated by the factory
public class Session : IDisposable
{
    private readonly string _id;
    private readonly ILogger<Session> _logger;
    private readonly InjectOutcomePolicy _faultPolicy;
    private readonly InjectLatencyPolicy _latencyPolicy;
    private readonly PolicyWrap _faultLatencyPolicy;
    private int _disposed;

    public Session(string id, ILogger<Session> logger)
    {
        _id = id;
        _logger = logger;
        _faultPolicy = MonkeyPolicy.InjectException(with =>
            with.Fault(new InvalidOperationException("Cannot start session, Media Platform is down"))
                .InjectionRate(0.4)
                .Enabled(true)
        );
        _latencyPolicy = MonkeyPolicy.InjectLatency(with =>
            with.Latency(TimeSpan.FromSeconds(4))
                .InjectionRate(0.7)
                .Enabled(true)
        );
        _faultLatencyPolicy = Policy.Wrap(_faultPolicy, _latencyPolicy);
    }

    public void Start()
    {
        _faultLatencyPolicy.Execute(() => _logger.LogInformation("Session with ID: {SessionId} started", _id));
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
