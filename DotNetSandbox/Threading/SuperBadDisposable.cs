namespace Threading;

public class SuperBadDisposable : IDisposableWithCounter
{
    private int _counter;

    public int Counter => _counter;

    public void Dispose()
    {
        Interlocked.Increment(ref _counter);
    }
}
