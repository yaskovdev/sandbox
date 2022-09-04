namespace StatefulService;

public interface ISessionRepository
{
    Session GetSession(string id);

    void CreateSession(Session session);
}
