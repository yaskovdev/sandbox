namespace AspNetSandbox;

public class StatefulServiceFactory(IServiceProvider serviceProvider) : IStatefulServiceFactory
{
    public StatefulService Create(SocketId socketId) =>
        ActivatorUtilities.CreateInstance<StatefulService>(serviceProvider, socketId);
}