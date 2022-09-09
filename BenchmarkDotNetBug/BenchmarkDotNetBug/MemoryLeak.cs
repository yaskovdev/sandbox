namespace BenchmarkDotNetBug;

using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

[KeepBenchmarkFiles]
[NativeMemoryProfiler]
public class MemoryLeak
{
    [Benchmark]
    public void Run()
    {
        var pointer = Marshal.AllocHGlobal(sizeof(byte) * 10);
    }
}
