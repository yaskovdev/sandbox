namespace AspNetSandbox;

/// <summary>
/// Note that it does not contain any pass-through dependencies, i.e., dependencies that it does not use, but that the
/// <see cref="SocketHandler"/> class uses (e.g., <see cref="ISingletonDependency"/> or <see cref="ILogger{StatefulService}"/>).
/// The <see cref="SocketHandlerFactory"/> does not contain those dependencies either.
/// </summary>
/// <param name="socketHandlerFactory"></param>
public class StatelessService(ISocketHandlerFactory socketHandlerFactory) : IStatelessService
{
    public void Process(SocketId socketId)
    {
        var socketHandler = socketHandlerFactory.CreateSocketHandler(socketId);
        socketHandler.Start();
    }
}