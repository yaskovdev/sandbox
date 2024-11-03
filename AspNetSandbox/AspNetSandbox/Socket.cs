using Timer = System.Timers.Timer;

namespace AspNetSandbox;

public class Socket : IDisposable
{
    private readonly Timer _timer;
    private int _disposed;

    public Socket(SocketId id, Action<SocketId, byte[]> processData)
    {
        _timer = new Timer(TimeSpan.FromSeconds(1));
        _timer.Elapsed += (_, _) =>
        {
            var data = new byte[1024];
            Random.Shared.NextBytes(data);
            processData(id, data);
        };
        _timer.Start();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
        {
            return;
        }

        if (disposing)
        {
            _timer.Dispose();
        }
    }
}
