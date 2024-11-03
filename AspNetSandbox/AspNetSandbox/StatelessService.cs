namespace AspNetSandbox;

/// <summary>
/// Note that it does not contain any pass-through dependencies, i.e., dependencies that it does not use, but that the
/// <see cref="StatefulService"/> class uses (e.g., <see cref="ISingletonDependency"/> or <see cref="ILogger{StatefulService}"/>).
/// </summary>
/// <param name="statefulServiceFactory"></param>
public class StatelessService(IStatefulServiceFactory statefulServiceFactory) : IStatelessService
{
    public void Process(SocketId socketId)
    {
        var statefulService = statefulServiceFactory.CreateStatefulService(socketId);
        statefulService.Handle();
    }
}