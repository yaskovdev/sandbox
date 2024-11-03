namespace AspNetSandbox;

using System.Security.Cryptography;

public class SocketHandler : IDisposable
{
    private readonly ISingletonDependency _singletonDependency;
    private readonly ILogger<SocketHandler> _logger;
    private readonly Socket _socket;
    private int _disposed;

    public SocketHandler(ISingletonDependency singletonDependency,
        ILogger<SocketHandler> logger,
        SocketId socketId)
    {
        _singletonDependency = singletonDependency;
        _logger = logger;
        _socket = new Socket(socketId, ProcessData);
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
            _socket.Dispose();
        }
    }

    private void ProcessData(SocketId socketId, byte[] data)
    {
        using (var algorithm = SHA256.Create())
        {
            _logger.LogInformation("Processing data with hash {Hash} from socket {SocketId}", GetHash(algorithm, data), socketId);
        }

        _singletonDependency.Handle(socketId);
    }

    private static string GetHash(HashAlgorithm algorithm, byte[] input) =>
        string.Join("", algorithm.ComputeHash(input).Select(it => it.ToString("x2")));
}
