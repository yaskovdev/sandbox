namespace MediaApp;

public interface ISessionFactory
{
    Session CreateSession(string sessionId);
}
