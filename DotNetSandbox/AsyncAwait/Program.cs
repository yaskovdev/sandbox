using System;
using System.Threading;

namespace AsyncAwait;

using System.Threading.Tasks;

internal static class Program
{
    private static async Task Main()
    {
        // var sandbox = new Sandbox();
        // sandbox.Connect(false).ContinueWith(connection => Console.WriteLine(connection.Result));
        // Console.WriteLine("Hello World!");
        
        // ----

        // var service = new LongRunningService();
        // await service.Run();
        // var bytes = new int[1000000];
        // foreach(var item in bytes)
        // {
        //     Console.WriteLine(item.ToString());
        // }
        
        // ----

        var service = new AsyncMethodChainService();
        service.StartRecordingMode();
        Thread.Sleep(3000);
    }
    
    private static async Task RunAsync()
    {
        await Task.CompletedTask;
    }
    
    private static async Task<int> GetNumberAsync()
    {
        return await Task.FromResult(42);
    }
}
