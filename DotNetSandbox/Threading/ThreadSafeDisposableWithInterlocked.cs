namespace Threading;

public class ThreadSafeDisposableWithInterlocked : IDisposableWithCounter
{
    private int _counter;
    private int _disposed;

    public int Counter => _counter;

    public void Dispose()
    {
        if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1) return;
        Interlocked.Increment(ref _counter);
    }
}
