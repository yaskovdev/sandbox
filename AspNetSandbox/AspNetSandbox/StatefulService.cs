namespace AspNetSandbox;

public class StatefulService(
    ISingletonDependency singletonDependency,
    ILogger<StatefulService> logger,
    SocketId socketId)
    : IStatefulService
{
    public void Handle()
    {
        logger.LogInformation("Handling socket {SocketId} using {Dependency}", socketId, singletonDependency);
    }
}