using System;

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

        var service = new LongRunningService();
        await service.Run();
    }
}
