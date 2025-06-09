namespace MediaApp;

public interface ISessionService
{
    uint SessionCount { get; }

    Outcome CreateCall(string callId);

    string TransferSession(string callId, string sessionId);
}
