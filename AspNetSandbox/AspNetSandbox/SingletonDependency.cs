namespace AspNetSandbox;

public class SingletonDependency(ILogger<SingletonDependency> logger) : ISingletonDependency
{
    public void Handle(SocketId socketId)
    {
        logger.LogInformation("Handling socket {SocketId}", socketId);
    }
}