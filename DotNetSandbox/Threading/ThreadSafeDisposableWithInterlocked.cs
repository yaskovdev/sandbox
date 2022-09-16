namespace Threading;

public class ThreadSafeDisposableWithInterlocked : IDisposableWithCounter
{
    private int _counter;
    private int _disposeStarted;

    public int Counter => _counter;

    public void Dispose()
    {
        if (Interlocked.CompareExchange(ref _disposeStarted, 1, 0) == 1)
        {
            Interlocked.Increment(ref _counter);
        }
    }
}
