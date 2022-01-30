using BenchmarkDotNet.Running;

namespace ForEachVsLinqSumPerformance;

static class Program
{
    private static void Main()
    {
        var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        Console.WriteLine(summary);
    }
}
