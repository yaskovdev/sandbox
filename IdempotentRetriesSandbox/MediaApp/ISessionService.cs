namespace MediaApp;

public interface ISessionService
{
    int SessionCount { get; }

    Outcome CreateCall(string callId);

    void TransferSession(string callId, string sessionId);

    void DeleteSession(string sessionId);
}
