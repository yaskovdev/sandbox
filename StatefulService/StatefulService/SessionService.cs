namespace StatefulService;

public class SessionService : ISessionService
{
    private readonly IDictionary<string, Session> sessions = new Dictionary<string, Session>();

    public Session GetSession(string id) => sessions[id];

    public Session CreateSession(string id)
    {
        var session = new Session();
        sessions[id] = session;
        return session;
    }

    public Session RegisterEvent(string sessionId, SessionEvent sessionEvent)
    {
        var session = GetSession(sessionId);
        session.NumberOfEvents += 1;
        session.TotalDurationOfEvents += sessionEvent.Duration;
        return session;
    }
}
