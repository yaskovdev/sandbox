namespace AspNetSandbox;

using System.Collections.Concurrent;

/// <summary>
/// Note that it does not contain any pass-through dependencies, i.e., dependencies that it does not use, but that the
/// <see cref="SocketHandler"/> class uses (e.g., <see cref="ISingletonDependency"/> or <see cref="ILogger{SocketHandler}"/>).
/// The <see cref="SocketHandlerFactory"/> does not contain those dependencies either.
/// </summary>
public class DataProcessingService(ISocketHandlerFactory socketHandlerFactory, ILogger<DataProcessingService> logger)
    : IDataProcessingService
{
    private readonly ConcurrentDictionary<SocketId, SocketHandler> _socketHandlers = new();

    public void StartProcessing(SocketId socketId)
    {
        _socketHandlers.GetOrAdd(socketId, socketHandlerFactory.CreateSocketHandler);
    }

    public void StopProcessing(SocketId socketId)
    {
        if (_socketHandlers.TryRemove(socketId, out var socketHandler))
        {
            socketHandler.Dispose();
        }
        else
        {
            logger.LogWarning("Socket handler for socket ID {SocketId} not found", socketId);
        }
    }
}
