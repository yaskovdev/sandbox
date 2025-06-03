namespace MediaApp;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using StackExchange.Redis;

public class SessionService : ISessionService, IAsyncDisposable
{
    private static readonly TimeSpan LeaseExtensionPeriod = TimeSpan.FromSeconds(6);
    private readonly string _createCallWithSessionIfNotExists = ReadResource("CreateCallWithSessionIfNotExists.lua");
    private readonly string _transferSession = ReadResource("TransferSession.lua");
    private readonly ConcurrentDictionary<string, Session> _sessions = new();
    private readonly IConnectionMultiplexer _redis;
    private readonly ISessionFactory _sessionFactory;
    private readonly ILogger<SessionService> _logger;
    private readonly Timer _timer;

    public SessionService(IConnectionMultiplexer redis, ISessionFactory sessionFactory, ILogger<SessionService> logger)
    {
        _redis = redis;
        _sessionFactory = sessionFactory;
        _logger = logger;
        _timer = new Timer(ExtendSessionsLease, _sessions, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    // Call ID is used as the idempotency token.
    public Outcome CreateCall(string callId)
    {
        var newSessionId = Guid.NewGuid().ToString();
        var database = _redis.GetDatabase();
        var now = DateTime.Now;
        var newSession = new SessionEntity(callId, SessionState.Active, now, now + LeaseExtensionPeriod);
        var result = (int)database.ScriptEvaluate(_createCallWithSessionIfNotExists, ["call:" + callId, "session:" + newSessionId], [JsonSerializer.Serialize(newSession)]);

        if (result == 0)
        {
            return Outcome.Unchanged;
        }

        var session = _sessionFactory.CreateSession(newSessionId);
        if (_sessions.TryAdd(newSessionId, session))
        {
            session.Start();
        }
        else
        {
            Debug.Fail("Session with ID " + newSessionId + " already exists in the dictionary. This should never happen, because the session ID is a GUID");
            session.Dispose();
        }

        return Outcome.Created;
    }

    /// <summary>
    /// Transactionally inactivates the existing session (to make it not eligible for Watchdog) and creates a new one.
    /// </summary>
    public void TransferSession(string callId, string sessionId)
    {
        // TODO: create a session with a new ID
        var newSessionId = Guid.NewGuid().ToString();
        var now = DateTime.Now;
        var newSession = new SessionEntity(callId, SessionState.Active, now, now + LeaseExtensionPeriod);

        var database = _redis.GetDatabase();
        database.ScriptEvaluate(_transferSession, ["call:" + callId, "session:" + sessionId, "session:" + newSessionId], [JsonSerializer.Serialize(newSession)]);

        var session = _sessionFactory.CreateSession(newSessionId);
        if (_sessions.TryAdd(newSessionId, session))
        {
            session.Start();
        }
        else
        {
            // This may happen if the instance fails to extend the session lease and then Watchdog transfer it to the same instance.
            session.Dispose();
        }
    }

    public void DeleteSession(string sessionId)
    {
        if (_sessions.TryRemove(sessionId, out var session))
        {
            session.Dispose();
            var database = _redis.GetDatabase();
            // TODO: probably should change state to Finished instead of deleting the session
            database.KeyDelete("session:" + sessionId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _timer.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    private void ExtendSessionsLease(object? state)
    {
        IDictionary<string, Session>? sessions = (ConcurrentDictionary<string, Session>?)state;
        var sessionIds = (sessions ?? ImmutableDictionary<string, Session>.Empty).Keys.ToImmutableArray();
        foreach (var sessionId in sessionIds)
        {
            ExtendSessionLease(sessionId);
        }
    }

    // TODO: should it be atomic?
    // TODO: should the timer wait for the previous execution to finish? If it does not wait, then we may overwhelm the Redis server with requests.
    // If it waits, then there is a bigger chance for Watchdog to falsely declare the session stale, if, say, only 50% of Redis requests have a long latency.
    private void ExtendSessionLease(string sessionId)
    {
        _logger.LogInformation("Extending a lease for the session with ID: {SessionId}", sessionId);
        var database = _redis.GetDatabase();
        var value = database.StringGet("session:" + sessionId);
        if (value.IsNull)
            return;

        var session = JsonSerializer.Deserialize<SessionEntity>((string)value);
        if (session == null)
            return;

        var updatedSession = session with { LeaseExpiresAt = DateTime.Now + LeaseExtensionPeriod };
        database.StringSet("session:" + sessionId, JsonSerializer.Serialize(updatedSession));
        // ChaosMonkeyPolicies.LatencyPolicy(TimeSpan.FromSeconds(10), 0)
        //     .Execute(() =>
        //     {
        //     });
    }

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }
}
