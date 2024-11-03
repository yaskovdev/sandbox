namespace AspNetSandbox;

public interface IStatefulServiceFactory
{
    IStatefulService CreateStatefulService(SocketId socketId);
}