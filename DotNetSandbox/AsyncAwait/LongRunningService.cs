namespace AsyncAwait;

using System.Threading.Tasks;

public class LongRunningService
{
    public async Task Run()
    {
        await Task.Delay(-1);
    }
}
