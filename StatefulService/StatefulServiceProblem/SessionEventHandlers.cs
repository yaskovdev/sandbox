namespace StatefulServiceProblem;

public class SessionEventHandlers : ISessionEventHandlers
{
    private readonly IDictionary<string, ISessionEventHandler> handlers = new Dictionary<string, ISessionEventHandler>();

    public void Add(string sessionId, ISessionEventHandler handler) => handlers[sessionId] = handler;

    public ISessionEventHandler Get(string sessionId) => handlers[sessionId];
}
