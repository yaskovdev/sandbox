namespace MediaApp;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using StackExchange.Redis;

public class SessionService : ISessionService, IAsyncDisposable
{
    private readonly string _getAndActivateSessionOrCreate = ReadResource("GetAndActivateSessionOrCreate.lua");
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

    public SessionEntity CreateSession(string sessionId)
    {
        var database = _redis.GetDatabase();
        var now = DateTime.Now;
        var newSession = new SessionEntity(SessionState.Active, now, now);
        var newSessionJson = JsonSerializer.Serialize(newSession);
        var result = (RedisValue[])database.ScriptEvaluate(_getAndActivateSessionOrCreate, ["session:" + sessionId], [newSessionJson]);
        var sessionEntity = JsonSerializer.Deserialize<SessionEntity>((string)result[1]);

        // It is important to claim (activate) the session in Redis first before starting any processing.
        if (SessionHasBeenCreatedOrActivated((int)result[0]))
        {
            var session = _sessionFactory.CreateSession(sessionId);
            if (_sessions.TryAdd(sessionId, session))
            {
                session.Start();
            }
            else
            {
                Debug.Fail("Session with ID " + sessionId + " already exists in the dictionary. This should never happen, because the Redis check should prevent this");
                session.Dispose();
            }
        }
        else
        {
            _logger.LogWarning("Session with ID {SessionId} already exists in Redis in {State} state, ignoring the creation request", sessionId, SessionState.Active);
        }

        return sessionEntity;
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

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }

    private static bool SessionHasBeenCreatedOrActivated(int status) => status == 1;
}
