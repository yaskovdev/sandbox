namespace StatefulService;

public class SessionService : ISessionService
{
    private readonly ISessionRepository repository;

    public SessionService(ISessionRepository repository) => this.repository = repository;

    public Session GetSession(string id) => repository.GetSession(id);

    public Session CreateSession(string id)
    {
        var session = new Session { Id = id };
        repository.CreateSession(session);
        return session;
    }
}
