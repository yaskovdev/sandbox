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
        Console.WriteLine("NoLock disposed after " + _iteration + " iterations");
        _iteration = 0;
    }
}

public class LockService : IDisposable
{
    private readonly object _lock = new object();
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
            Console.WriteLine("NoLock disposed after " + _iteration + " iterations");
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
            Console.WriteLine("NoLock disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
    }
}

public class SemaphoreService : IAsyncDisposable
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
            Console.WriteLine("NoLock disposed after " + _iteration + " iterations");
            _iteration = 0;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}

public class NoLockVsLockVsAsyncLockVsSemaphore
{
    private NoLockService _noLockService;
    private LockService _lockService;
    private AsyncLockService _asyncLockService;
    private SemaphoreService _semaphoreService;

    [GlobalSetup]
    public void Setup()
    {
        _noLockService = new NoLockService();
        _lockService = new LockService();
        _asyncLockService = new AsyncLockService();
        _semaphoreService = new SemaphoreService();
    }

    [Benchmark]
    public void NoLockService()
    {
        for (var i = 0; i < 1000000; i++)
        {
            _noLockService.Process();
        }
    }

    [Benchmark]
    public void LockService()
    {
        for (var i = 0; i < 1000000; i++)
        {
            _lockService.Process();
        }
    }

    [Benchmark]
    public async Task AsyncLockService()
    {
        for (var i = 0; i < 1000000; i++)
        {
            await _asyncLockService.Process();
        }
    }

    [Benchmark]
    public async Task SemaphoreService()
    {
        for (var i = 0; i < 1000000; i++)
        {
            await _semaphoreService.Process();
        }
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        _noLockService.Dispose();
        _lockService.Dispose();
        await _asyncLockService.DisposeAsync();
        await _semaphoreService.DisposeAsync();
    }
}
