using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;

namespace ForEachVsLinqSumPerformance;

public class Benchmark
{
    private const int N = 100000;
    private const long Max = 100000;

    private readonly IEnumerable<long> numbers;

    public Benchmark()
    {
        var random = new Random();
        numbers = Enumerable
            .Repeat(0, N)
            .Select(_ => random.NextInt64(-Max, Max))
            .ToImmutableList();
    }

    [Benchmark]
    public long ForEachSum()
    {
        long sum = 0;
        foreach (var number in numbers) sum += number;
        return sum;
    }

    [Benchmark]
    public long ParallelLinqSum() => numbers.AsParallel().Sum();
}
