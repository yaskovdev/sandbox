namespace MediaApp;

public interface ISessionService
{
    int CreateSession(string sessionId);

    void DeleteSession(string sessionId);
}
