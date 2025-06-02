namespace MediaApp;

public interface ISessionService
{
    SessionEntity CreateSession();

    void AssignSession(string sessionId);

    void DeleteSession(string sessionId);
}
