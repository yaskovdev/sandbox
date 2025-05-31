namespace IdempotentRetriesSandbox;

using System.Collections.Concurrent;
using System.Text.Json;
using StackExchange.Redis;

public class ProcessingService(IConnectionMultiplexer redis, ISessionFactory sessionFactory) : IProcessingService
{
    private readonly string _setIfNotExistsElseGet = ReadResource("SetIfNotExistsElseGet.lua");
    private readonly ConcurrentDictionary<string, Session> _sessions = new();

    public SessionEntity CreateSession(string sessionId)
    {
        var database = redis.GetDatabase();
        var now = DateTime.Now;
        var newProcessing = new SessionEntity(now, now);
        var newProcessingJson = JsonSerializer.Serialize(newProcessing);
        var result = database.ScriptEvaluate(_setIfNotExistsElseGet, [sessionId], [newProcessingJson]);
        var sessionEntity = result.IsNull ? newProcessing : JsonSerializer.Deserialize<SessionEntity>((string)result);
        var session = sessionFactory.CreateSession(sessionId);
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
            var database = redis.GetDatabase();
            database.KeyDelete(sessionId);
        }
    }

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }
}
