using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

namespace ForEachVsLinqSumPerformance;

public class Benchmark
{
    private const int N = 10000;
    private const int Max = 1000;

    private readonly IEnumerable<int> numbers;

    public Benchmark()
    {
        var random = new Random();
        numbers = Enumerable
            .Repeat(0, N)
            .Select(_ => random.Next(-Max, Max))
            .ToImmutableList();
    }

    [Benchmark]
    public int ForEachSum()
    {
        var sum = 0;
        foreach (var number in numbers)
        {
            sum += number;
        }

        return sum;
    }

    [Benchmark]
    public int LinqSum() => numbers.Sum();
}
