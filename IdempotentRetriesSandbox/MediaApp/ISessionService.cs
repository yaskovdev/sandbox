namespace MediaApp;

public interface ISessionService
{
    Outcome CreateCall(string callId);

    void AssignSession(string sessionId);

    void DeleteSession(string sessionId);
}
