using BenchmarkDotNet.Attributes;

namespace Benchmarks;

public class TryCatchVsNoTryCatch
{
    [Benchmark]
    public void TryCatch()
    {
        try
        {
            for (var i = 0; i < 1000000000; i++)
            {
                var d = Math.Sin(1);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [Benchmark]
    public void NoTryCatch()
    {
        for (var i = 0; i < 1000000000; i++)
        {
            var d = Math.Sin(1);
        }
    }
}