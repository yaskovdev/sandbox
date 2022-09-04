namespace StatefulService;

public class SessionService : ISessionService
{
    private readonly ISessionRepository repository;

    public SessionService(ISessionRepository repository) => this.repository = repository;

    public Session GetSession(string id) => repository.GetSession(id);

    public void CreateSession(string id) => repository.CreateSession(new Session { Id = id });
}
