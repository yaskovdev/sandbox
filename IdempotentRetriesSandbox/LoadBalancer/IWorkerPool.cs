namespace LoadBalancer;

public interface IWorkerPool
{
    Uri? ReserveWorker();

    void ReleaseWorker(Uri workerUri, WorkerStatus newStatus);
}
