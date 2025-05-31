namespace IdempotentRetriesSandbox;

using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Text.Json;
using StackExchange.Redis;

public class SessionService : ISessionService, IAsyncDisposable
{
    private readonly string _setIfNotExistsElseGet = ReadResource("SetIfNotExistsElseGet.lua");
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
        _timer = new Timer(UpdateSessions, _sessions, TimeSpan.Zero, TimeSpan.FromSeconds(3));
    }

    public SessionEntity CreateSession(string sessionId)
    {
        var database = _redis.GetDatabase();
        var now = DateTime.Now;
        var newProcessing = new SessionEntity(now, now);
        var newProcessingJson = JsonSerializer.Serialize(newProcessing);
        var result = database.ScriptEvaluate(_setIfNotExistsElseGet, [sessionId], [newProcessingJson]);
        var sessionEntity = result.IsNull ? newProcessing : JsonSerializer.Deserialize<SessionEntity>((string)result);
        var session = _sessionFactory.CreateSession(sessionId);
        if (!_sessions.TryAdd(sessionId, session))
        {
            session.Dispose();
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
            database.KeyDelete(sessionId);
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

    private void UpdateSession(string sessionId)
    {
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

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }
}
