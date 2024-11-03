using Timer = System.Timers.Timer;

namespace AspNetSandbox;

public class Socket : IDisposable
{
    private readonly Timer _timer;

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
        _timer.Dispose();
        GC.SuppressFinalize(this);
    }
}
