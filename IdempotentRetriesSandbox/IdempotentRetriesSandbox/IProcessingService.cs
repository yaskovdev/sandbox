namespace IdempotentRetriesSandbox;

public interface IProcessingService
{
    SessionEntity CreateSession(string sessionId);

    void DeleteSession(string sessionId);
}
