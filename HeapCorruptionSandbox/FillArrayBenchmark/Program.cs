namespace FillArrayBenchmark;

using BenchmarkDotNet.Running;

internal static class Program
{
    public static void Main()
    {
        var summary = BenchmarkRunner.Run<FillArrayBenchmark>();
        Console.WriteLine(summary);
    }
}
