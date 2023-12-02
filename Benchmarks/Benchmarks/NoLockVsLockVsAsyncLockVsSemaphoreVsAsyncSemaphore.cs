namespace Benchmarks;

using BenchmarkDotNet.Attributes;
using Nito.AsyncEx;

public class NoLockService : IDisposable
{
    private long _iteration;

    public void Process()
    {
        _iteration += 1;
    }

    public void Dispose()
    {
        Console.WriteLine("NoLockService disposed after " + _iteration + " iterations");
        _iteration = 0;
    }
}

public class LockService : IDisposable
{
    private readonly object _lock = new();
    private long _iteration;

    public void Process()
    {
        lock (_lock)
        {
            _iteration += 1;
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            Console.WriteLine("LockService disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
    }
}

public class AsyncLockService : IAsyncDisposable
{
    private readonly AsyncLock _asyncLock = new();
    private long _iteration;

    public async Task Process()
    {
        using (await _asyncLock.LockAsync())
        {
            _iteration += 1;
        }
    }

    public async ValueTask DisposeAsync()
    {
        using (await _asyncLock.LockAsync())
        {
            Console.WriteLine("AsyncLockService disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
    }
}

public class SemaphoreService : IDisposable
{
    private readonly Semaphore _semaphore = new(1, 1);
    private long _iteration;

    public void Process()
    {
        try
        {
            _semaphore.WaitOne();
            _iteration += 1;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        try
        {
            _semaphore.WaitOne();
            Console.WriteLine("SemaphoreService disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

public class AsyncSemaphoreService : IAsyncDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private long _iteration;

    public async Task Process()
    {
        try
        {
            await _semaphore.WaitAsync();
            _iteration += 1;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await _semaphore.WaitAsync();
            Console.WriteLine("AsyncSemaphoreService disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

public class NoLockVsLockVsAsyncLockVsSemaphoreVsAsyncSemaphore
{
    private NoLockService _noLockService;
    private LockService _lockService;
    private AsyncLockService _asyncLockService;
    private SemaphoreService _semaphoreService;
    private AsyncSemaphoreService _asyncSemaphoreService;

    [GlobalSetup]
    public void Setup()
    {
        _noLockService = new NoLockService();
        _lockService = new LockService();
        _asyncLockService = new AsyncLockService();
        _semaphoreService = new SemaphoreService();
        _asyncSemaphoreService = new AsyncSemaphoreService();
    }

    [Benchmark]
    public void NoLockService()
    {
        _noLockService.Process();
    }

    [Benchmark]
    public void LockService()
    {
        _lockService.Process();
    }

    [Benchmark]
    public async Task AsyncLockService()
    {
        await _asyncLockService.Process();
    }

    [Benchmark]
    public void SemaphoreService()
    {
        _semaphoreService.Process();
    }

    [Benchmark]
    public async Task AsyncSemaphoreService()
    {
        await _asyncSemaphoreService.Process();
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        _noLockService.Dispose();
        _lockService.Dispose();
        await _asyncLockService.DisposeAsync();
        _semaphoreService.Dispose();
        await _asyncSemaphoreService.DisposeAsync();
    }
}
