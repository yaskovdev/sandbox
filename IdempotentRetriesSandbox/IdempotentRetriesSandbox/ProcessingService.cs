namespace IdempotentRetriesSandbox;

using System.Text.Json;
using StackExchange.Redis;

public class ProcessingService : IProcessingService
{
    private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect("localhost");
    private readonly string _setIfNotExistsElseGet = ReadResource("SetIfNotExistsElseGet.lua");

    public Processing StartProcessing(string sessionId)
    {
        var database = _redis.GetDatabase();
        var newProcessing = new Processing(DateTime.Now);
        var newProcessingJson = JsonSerializer.Serialize(newProcessing);
        var result = database.ScriptEvaluate(_setIfNotExistsElseGet, [sessionId], [newProcessingJson]);
        return result.IsNull ? newProcessing : JsonSerializer.Deserialize<Processing>((string)result);
    }

    private static string ReadResource(string resourceName)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resourceName);
        return File.ReadAllText(path);
    }
}
