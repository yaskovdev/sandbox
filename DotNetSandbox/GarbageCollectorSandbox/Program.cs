namespace GarbageCollectorSandbox;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting");
        AllocateManyObjects();
        Console.WriteLine("Done");
    }

    private static void AllocateManyObjects()
    {
        const int iterations = 20_000;
        for (var i = 0; i < iterations; i++)
        {
            var arr = new byte[64 * 1024 * 1024];
            if (i % 1_000 == 0)
            {
                Console.WriteLine($"Allocated array of size {arr.Length}, with element 0 being {arr[0]} at iteration {i}");
            }
        }
    }
}
