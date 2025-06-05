namespace LoadBalancer;

public interface IWorkerPool
{
    Task<WorkerView?> ReserveWorker();

    Task ReleaseWorker(Uri workerUri, uint newAvailableSlotCount);
}
