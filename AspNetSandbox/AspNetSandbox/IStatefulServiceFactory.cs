namespace AspNetSandbox;

public interface IStatefulServiceFactory
{
    StatefulService Create(SocketId socketId);
}