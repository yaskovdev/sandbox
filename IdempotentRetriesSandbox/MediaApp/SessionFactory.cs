namespace MediaApp;

public class SessionFactory(IServiceProvider serviceProvider) : ISessionFactory
{
    public Session CreateSession(string sessionId) =>
        serviceProvider.CreateInstance<Session>(sessionId);
}
