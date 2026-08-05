namespace Misc;

public class AsyncDeadlock
{
    public string GetData()
    {
        // Blocks the current thread (e.g., UI thread or request thread)
        var result = GetDataAsync().Result; // 💀 deadlock
        return result;
    }

    private async Task<string> GetDataAsync()
    {
        // After this await, the continuation wants to resume
        // on the original SynchronizationContext thread...
        // but that thread is blocked by .Result above!
        var client = new HttpClient();
        await client.GetStringAsync("https://yaskovdev.com");
        return "done";
    }
}