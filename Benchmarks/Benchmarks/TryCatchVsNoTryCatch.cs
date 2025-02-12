using BenchmarkDotNet.Attributes;

namespace Benchmarks;

public class TryCatchVsNoTryCatch
{
    [Benchmark]
    public void TryCatch()
    {
        try
        {
            var d = Math.Sin(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [Benchmark]
    public void NoTryCatch()
    {
        var d = Math.Sin(1);
    }
}