using BenchmarkDotNet.Running;
using Benchmarks;

Console.WriteLine("Running benchmarks");

Console.WriteLine(BenchmarkRunner.Run<NoLockVsLockVsAsyncLockVsSemaphoreVsAsyncSemaphore>());
