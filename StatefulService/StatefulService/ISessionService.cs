namespace StatefulService;

public interface ISessionService
{
    Session GetSession(string id);

    void CreateSession(string id);
}
