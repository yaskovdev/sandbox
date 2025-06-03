namespace MediaApp;

public interface ISessionService
{
    Outcome CreateCall(string callId);

    void TransferSession(string callId, string sessionId);

    void DeleteSession(string sessionId);
}
