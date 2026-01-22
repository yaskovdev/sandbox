namespace GarbageCollectorSandbox;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting");
        const int iterations = 500_000_000;
        for (var i = 0; i < iterations; i++)
        {
            // Allocate a small object and let it go out of scope immediately
            var arr = new byte[128];
            // Optionally, force GC every 1 million allocations
            if (i % 10_000_000 == 0)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.WriteLine($"GC forced at iteration {i}");
            }
        }
        Console.WriteLine("Done");
    }
}