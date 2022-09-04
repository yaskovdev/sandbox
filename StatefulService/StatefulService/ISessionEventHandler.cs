namespace StatefulService;

public interface ISessionEventHandler
{
    void HandleEvent(string sessionId, SessionEvent sessionEvent);
}
