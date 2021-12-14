using System;

namespace AsyncAwait;

static class Program
{
    private static void Main()
    {
        var sandbox = new Sandbox();
        sandbox.Connect(false).ContinueWith(connection => Console.WriteLine(connection.Result));
        Console.WriteLine("Hello World!");
    }
}
