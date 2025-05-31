namespace IdempotentRetriesSandbox;

public interface ISessionFactory
{
    Session CreateSession(string sessionId);
}
