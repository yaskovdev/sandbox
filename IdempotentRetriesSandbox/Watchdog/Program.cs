namespace Watchdog;

using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using StackExchange.Redis;

internal static class Program
{
    private static readonly PeriodicTimer Timer = new(TimeSpan.FromSeconds(30));
    private static readonly HttpClient HttpClient = new();

    public static async Task Main(string[] args)
    {
        var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
        var pollerTask = ReassignStaleSessions(redis);
        Console.WriteLine("Watchdog started. Press any key to exit...");
        Console.ReadKey();
        Timer.Dispose();
        await pollerTask;
    }

    private static async Task ReassignStaleSessions(ConnectionMultiplexer redis)
    {
        do
        {
            Console.WriteLine("Checking for sessions with expired leases...");
            var sessions = GetStaleSessions(redis).ToImmutableArray();
            if (sessions.IsEmpty)
            {
                continue;
            }

            Console.WriteLine("Transferring " + sessions.Length + " sessions with expired leases: [" + string.Join(", ", sessions) + "]");
            Parallel.ForEach(sessions, session =>
            {
                var requestUri = $"http://localhost:5110/calls/{session.CallId}/sessions/{session.Id}/transfer";
                var content = new StringContent(JsonSerializer.Serialize(session), Encoding.UTF8, "application/json");
                try
                {
                    var response = HttpClient.PostAsync(requestUri, content).GetAwaiter().GetResult();
                    Console.WriteLine(response.IsSuccessStatusCode ? $"Successfully transferred session {session.Id}" : $"Failed to transfer session {session.Id}: {response.StatusCode}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Failed to assign session {session.Id}: {e.Message}");
                }
            });
        } while (await Timer.WaitForNextTickAsync());
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
            if (session.State == SessionState.Active && now >= session.LeaseExpiresAt)
            {
                yield return new Session(sessionKey.Split(':')[1], session.CallId, session.CreatedAt, session.LeaseExpiresAt);
            }
        }
    }

    private static ImmutableArray<string> GetSessionKeys(ConnectionMultiplexer redis)
    {
        var server = redis.GetServer(redis.GetEndPoints().First());
        return [..server.Keys(pattern: "session:*", pageSize: 100).Select(key => key.ToString())];
    }
}
