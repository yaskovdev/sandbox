namespace IdempotentRetriesSandbox;

public interface IProcessingService
{
    Processing StartProcessing(string sessionId);
}
