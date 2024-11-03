namespace AspNetSandbox;

public class StatefulServiceFactory(IServiceProvider serviceProvider) : IStatefulServiceFactory
{
    // You could create a new instance of StatefulService here, but then you would have to inject its dependencies into the factory.
    public IStatefulService CreateStatefulService(SocketId socketId) =>
        serviceProvider.CreateInstance<StatefulService>(socketId);
}