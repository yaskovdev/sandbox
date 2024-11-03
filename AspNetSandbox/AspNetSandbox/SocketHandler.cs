namespace AspNetSandbox;

public class SocketHandler(
    ISingletonDependency singletonDependency,
    ILogger<SocketHandler> logger,
    SocketId socketId)
    : ISocketHandler
{
    public void Start()
    {
        logger.LogInformation("Started handling socket {SocketId} using {Dependency}", socketId, singletonDependency);
        singletonDependency.Handle(socketId);
    }
}