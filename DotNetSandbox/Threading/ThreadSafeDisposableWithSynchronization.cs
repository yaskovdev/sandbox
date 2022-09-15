namespace Threading;

using System.Runtime.CompilerServices;

public class ThreadSafeDisposableWithSynchronization : IDisposableWithCounter
{
    private int _counter;
    private volatile bool _disposed;

    public int Counter => _counter;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Dispose()
    {
        if (_disposed) return;
        Interlocked.Increment(ref _counter);
        _disposed = true;
    }
}
