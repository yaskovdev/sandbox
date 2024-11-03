namespace AspNetSandbox;

public interface IStatelessService
{
    void Process(SocketId socketId);
}