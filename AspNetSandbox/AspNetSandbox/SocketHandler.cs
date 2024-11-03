namespace AspNetSandbox;

public class SocketHandler : IDisposable
{
    private readonly ISingletonDependency _singletonDependency;
    private readonly ILogger<SocketHandler> _logger;
    private readonly Socket _socket;

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
        _socket.Dispose();
        GC.SuppressFinalize(this);
    }

    private void ProcessData(SocketId socketId, byte[] data)
    {
        _logger.LogInformation("Processing {Length} bytes data from socket {SocketId}", data.Length, socketId);
        _singletonDependency.Handle(socketId);
    }
}
