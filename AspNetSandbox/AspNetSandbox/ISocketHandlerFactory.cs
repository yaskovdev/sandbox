namespace AspNetSandbox;

public interface ISocketHandlerFactory
{
    ISocketHandler CreateSocketHandler(SocketId socketId);
}