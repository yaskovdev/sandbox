namespace StatefulService;

public interface ISessionService
{
    Session GetSession(string id);

    Session CreateSession(string id);

    Session RegisterEvent(string sessionId, SessionEvent sessionEvent);
}
