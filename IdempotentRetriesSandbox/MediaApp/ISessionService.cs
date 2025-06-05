namespace MediaApp;

public interface ISessionService
{
    uint SessionCount { get; }

    Outcome CreateCall(string callId);

    void TransferSession(string callId, string sessionId);

    void DeleteSession(string sessionId);
}
