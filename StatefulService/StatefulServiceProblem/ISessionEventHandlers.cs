namespace StatefulServiceProblem;

public interface ISessionEventHandlers
{
    void Add(string sessionId, ISessionEventHandler handler);

    ISessionEventHandler Get(string sessionId);
}
