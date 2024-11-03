namespace AspNetSandbox;

public interface ISocketHandlerFactory
{
    SocketHandler CreateSocketHandler(SocketId socketId);
}
