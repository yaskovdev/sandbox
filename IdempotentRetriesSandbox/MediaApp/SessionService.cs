namespace MediaApp;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using StackExchange.Redis;

public class SessionService : ISessionService, IAsyncDisposable
{
    private static readonly TimeSpan LeaseExtensionPeriod = TimeSpan.FromSeconds(30);
    private static readonly string ScriptCreateCallWithSessionIfNotExists = ReadResource("CreateCallWithSessionIfNotExists.lua");
    private static readonly string ScriptTransferSession = ReadResource("TransferSession.lua");

    // TODO: problem, the session that was transferred away still stays in the dictionary and occupies a slot.
    // Need a reliable mechanism to remove the session from the dictionary eventually.
    private readonly ConcurrentDictionary<string, Session> _sessions = new();
    private readonly IConnectionMultiplexer _redis;
    private readonly ISessionFactory _sessionFactory;
    private readonly ILogger<SessionService> _logger;
    private readonly PeriodicTimer _timer;
    private readonly Task _pollerTask;

    public SessionService(IConnectionMultiplexer redis, ISessionFactory sessionFactory, ILogger<SessionService> logger)
    {
        _redis = redis;
        _sessionFactory = sessionFactory;
        _logger = logger;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(20));
        _pollerTask = ExtendSessionsLeaseSafe();
    }

    public uint SessionCount => (uint)_sessions.Count;

    // Call ID is used as the idempotency token.
    public Outcome CreateCall(string callId)
    {
        var newSessionId = Guid.NewGuid().ToString();
        var database = _redis.GetDatabase();
        var now = DateTime.Now;
        var newSession = new SessionEntity(callId, SessionState.Active, now, now + LeaseExtensionPeriod);
        var result = (int)database.ScriptEvaluate(ScriptCreateCallWithSessionIfNotExists, ["call:" + callId, "session:" + newSessionId], [JsonSerializer.Serialize(newSession)]);

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
    public string TransferSession(string callId, string sessionId)
    {
        var newSessionId = Guid.NewGuid().ToString();
        var now = DateTime.Now;
        var newSession = new SessionEntity(callId, SessionState.Active, now, now + LeaseExtensionPeriod);

        var database = _redis.GetDatabase();
        database.ScriptEvaluate(ScriptTransferSession, ["call:" + callId, "session:" + sessionId, "session:" + newSessionId], [JsonSerializer.Serialize(newSession)]);

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

        return newSessionId;
    }

    public async ValueTask DisposeAsync()
    {
        _timer.Dispose();
        await _pollerTask;
        GC.SuppressFinalize(this);
    }

    private async Task ExtendSessionsLeaseSafe()
    {
        do
        {
            Parallel.ForEach(_sessions.Keys.ToImmutableArray(), ExtendSessionLeaseSafe);
        } while (await _timer.WaitForNextTickAsync());
    }

    // TODO: should it be atomic?
    // TODO: should the timer wait for the previous execution to finish? If it does not wait, then we may overwhelm the Redis server with requests.
    // If it waits, then there is a bigger chance for Watchdog to falsely declare the session stale, if, say, only 50% of Redis requests have a long latency.
    private void ExtendSessionLeaseSafe(string sessionId)
    {
        try
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
        }
        catch (Exception e)
        {
            // If the worker fails to extend the lease, it should stop processing the session to free the processing slot.
            // In the meantime, Watchdog will reassign the session to another worker.
            // TODO: what are the other options? Should the worker go offline?
            _logger.LogError(e, "Failed to extend lease for session with ID: {SessionId}", sessionId);
            if (_sessions.TryRemove(sessionId, out var session))
            {
                session.Dispose();
            }
        }
    }

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }
}
