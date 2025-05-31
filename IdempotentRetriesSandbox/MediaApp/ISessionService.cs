namespace MediaApp;

public interface ISessionService
{
    SessionEntity CreateSession(string sessionId);

    void DeleteSession(string sessionId);
}
