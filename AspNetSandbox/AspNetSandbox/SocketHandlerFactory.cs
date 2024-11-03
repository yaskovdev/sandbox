namespace AspNetSandbox;

public class SocketHandlerFactory(IServiceProvider serviceProvider) : ISocketHandlerFactory
{
    // You could manually create a new instance of StatefulService here, but then you would have to inject its dependencies into the factory.
    public SocketHandler CreateSocketHandler(SocketId socketId) =>
        serviceProvider.CreateInstance<SocketHandler>(socketId);
}
