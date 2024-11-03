namespace AspNetSandbox;

/// <summary>
/// Note that it does not contain any pass-through dependencies, i.e., dependencies that it does not use, but that the
/// <see cref="SocketHandler"/> class uses (e.g., <see cref="ISingletonDependency"/> or <see cref="ILogger{SocketHandler}"/>).
/// The <see cref="SocketHandlerFactory"/> does not contain those dependencies either.
/// </summary>
/// <param name="socketHandlerFactory"></param>
public class DataProcessingService(ISocketHandlerFactory socketHandlerFactory) : IDataProcessingService
{
    public void StartProcessing(SocketId socketId)
    {
        var socketHandler = socketHandlerFactory.CreateSocketHandler(socketId);
        socketHandler.Start();
    }
}