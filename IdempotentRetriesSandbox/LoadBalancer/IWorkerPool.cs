namespace LoadBalancer;

public interface IWorkerPool
{
    Task<Uri?> ReserveWorker();

    Task ReleaseWorker(Uri workerUri, WorkerStatus newStatus);
}
