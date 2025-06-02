namespace MediaApp;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using StackExchange.Redis;

public class SessionService : ISessionService, IAsyncDisposable
{
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
        _timer = new Timer(UpdateSessions, _sessions, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    public SessionEntity CreateSession()
    {
        var sessionId = Guid.NewGuid().ToString();
        var database = _redis.GetDatabase();
        var now = DateTime.Now;
        var newSession = new SessionEntity(SessionState.Active, now, now);
        database.StringSet("session:" + sessionId, JsonSerializer.Serialize(newSession));

        var session = _sessionFactory.CreateSession(sessionId);
        if (_sessions.TryAdd(sessionId, session))
        {
            session.Start();
        }
        else
        {
            Debug.Fail("Session with ID " + sessionId + " already exists in the dictionary. This should never happen, because the session ID is a GUID");
            session.Dispose();
        }

        return newSession;
    }

    public void AssignSession(string sessionId)
    {
        var session = _sessionFactory.CreateSession(sessionId);
        if (_sessions.TryAdd(sessionId, session))
        {
            session.Start();
        }
        else
        {
            // This may happen if the instance failed to extend the session lease and then Watchdog reassigned it to the same instance.
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

    private void UpdateSessions(object? state)
    {
        IDictionary<string, Session>? sessions = (ConcurrentDictionary<string, Session>?)state;
        var sessionIds = (sessions ?? ImmutableDictionary<string, Session>.Empty).Keys.ToImmutableArray();
        foreach (var sessionId in sessionIds)
        {
            UpdateSession(sessionId);
        }
    }

    // TODO: should it be atomic?
    private void UpdateSession(string sessionId)
    {
        _logger.LogInformation("Updating session with ID: {SessionId}", sessionId);
        var database = _redis.GetDatabase();
        var value = database.StringGet("session:" + sessionId);
        if (value.IsNull)
            return;

        var session = JsonSerializer.Deserialize<SessionEntity>((string)value);
        if (session == null)
            return;

        var updatedSession = session with { UpdatedAt = DateTime.Now };
        database.StringSet("session:" + sessionId, JsonSerializer.Serialize(updatedSession));
    }
}
