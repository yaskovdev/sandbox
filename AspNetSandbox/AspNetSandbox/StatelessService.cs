namespace AspNetSandbox;

public class StatelessService(IStatefulServiceFactory statefulServiceFactory) : IStatelessService
{
    public void Process(SocketId socketId)
    {
        var statefulService = statefulServiceFactory.Create(socketId);
        statefulService.Handle();
    }
}