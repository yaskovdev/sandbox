namespace LoadBalancer;

public interface IPool
{
    Uri? GetAvailableWorker();
}
