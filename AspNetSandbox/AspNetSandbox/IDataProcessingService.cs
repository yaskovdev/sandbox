namespace AspNetSandbox;

public interface IDataProcessingService
{
    void StartProcessing(SocketId socketId);

    void StopProcessing(SocketId socketId);
}
