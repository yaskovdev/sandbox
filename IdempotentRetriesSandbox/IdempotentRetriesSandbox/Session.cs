namespace IdempotentRetriesSandbox;

using System.Text.Json;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;
using StackExchange.Redis;

// ReSharper disable once ClassNeverInstantiated.Global, this is instantiated by the factory
public class Session : IDisposable
{
    private readonly string _id;
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<Session> _logger;
    private readonly Timer _timer;
    private readonly InjectOutcomePolicy _chaosPolicy;
    private int _disposed;

    public Session(string id, IConnectionMultiplexer redis, ILogger<Session> logger)
    {
        _id = id;
        _redis = redis;
        _logger = logger;
        _timer = new Timer(UpdateSession, id, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        _chaosPolicy = MonkeyPolicy.InjectException(with =>
            with.Fault(new InvalidOperationException("Cannot start session, Media Platform is down"))
                .InjectionRate(0.5)
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
            _timer.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    private void UpdateSession(object? state)
    {
        var sessionId = (string)state;
        _logger.LogInformation("Updating session with ID: {SessionId}", sessionId);
        var database = _redis.GetDatabase();
        var value = database.StringGet(sessionId);
        if (value.IsNull)
            return;

        var session = JsonSerializer.Deserialize<SessionEntity>((string)value);
        if (session == null)
            return;

        var updatedSession = session with { UpdatedAt = DateTime.Now };
        database.StringSet(sessionId, JsonSerializer.Serialize(updatedSession));
    }
}
