using BenchmarkDotNet.Running;
using BenchmarkDotNetBug;

Console.WriteLine(BenchmarkRunner.Run<MemoryLeak>());
