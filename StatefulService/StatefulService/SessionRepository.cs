namespace StatefulService;

/// <summary>
/// In-memory version, but also can be a repository that stores the sessions in a database.
/// </summary>
public class SessionRepository : ISessionRepository
{
    private readonly IDictionary<string, Session> sessions = new Dictionary<string, Session>();

    public Session GetSession(string id) => sessions[id];

    public void CreateSession(Session session) => sessions[session.Id] = session;
}
