namespace Threading;

public class JustBadDisposable : IDisposableWithCounter
{
    private int _counter;
    private volatile bool _disposed;

    public int Counter => _counter;

    public void Dispose()
    {
        if (_disposed) return;
        Interlocked.Increment(ref _counter);
        _disposed = true;
    }
}
