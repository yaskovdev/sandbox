namespace Watchdog;

using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using StackExchange.Redis;

internal static class Program
{
    public static void Main(string[] args)
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        var timer = new Timer(ReassignStaleSessions, redis, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        Console.WriteLine("Watchdog started. Press any key to exit...");
        Console.ReadKey();
        timer.Dispose();
    }

    private static void ReassignStaleSessions(object? state)
    {
        Console.WriteLine("Checking for stale sessions...");
        var redis = (ConnectionMultiplexer)state;
        var database = redis.GetDatabase();
        foreach (var session in GetStaleSessions(redis))
        {
            Console.WriteLine("Found stale session: " + session);
            database.StringSet("session:" + session.Id, JsonSerializer.Serialize(new SessionEntity(SessionState.Inactive, session.CreatedAt, session.UpdatedAt)));
            var httpClient = new HttpClient();
            var requestUri = $"http://localhost:5032/sessions/{session.Id}";
            var content = new StringContent(JsonSerializer.Serialize(session), Encoding.UTF8, "application/json");
            try
            {
                var response = httpClient.PatchAsync(requestUri, content).GetAwaiter().GetResult();
                Console.WriteLine(response.IsSuccessStatusCode ? $"Successfully assigned session {session.Id}" : $"Failed to assign session {session.Id}: {response.StatusCode}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Failed to assign session {session.Id}: {e.Message}");
            }
        }
    }

    // TODO: read stale sessions from Redis right away, do not read all the keys
    private static IEnumerable<Session> GetStaleSessions(ConnectionMultiplexer redis)
    {
        var now = DateTime.Now;
        var database = redis.GetDatabase();
        foreach (var sessionKey in GetSessionKeys(redis))
        {
            var sessionJson = database.StringGet(sessionKey);
            var session = JsonSerializer.Deserialize<SessionEntity>(sessionJson);
            // TODO: in reality we additionally check if the assigned instance is processing this session, why?
            if (now - session.UpdatedAt >= TimeSpan.FromSeconds(6))
            {
                yield return new Session(sessionKey.Split(':')[1], session.CreatedAt, session.UpdatedAt);
            }
        }
    }

    private static ImmutableArray<string> GetSessionKeys(ConnectionMultiplexer redis)
    {
        var server = redis.GetServer(redis.GetEndPoints().First());
        return [..server.Keys(pattern: "session:*", pageSize: 100).Select(key => key.ToString())];
    }
}
