namespace LoadBalancer;

using System.Collections.Immutable;
using NeoSmart.AsyncLock;

public class WorkerPool : IWorkerPool, IAsyncDisposable
{
    private class Worker(Uri uri, uint availableSlotCount, bool reserved)
    {
        public AsyncLock Lock { get; } = new();

        public Uri Uri { get; } = uri;

        // We may want to distinguish between a healthy worker with 0 available slots and a faulty worker in the future.
        // The property should be accessed in a thread-safe manner.
        public uint AvailableSlotCount { get; set; } = availableSlotCount;

        /// <summary>
        /// Indicates that a worker is temporarily reserved. In this state, the worker is neither available 
        /// for new tasks nor can have its status updated by the poller.
        /// The property should be accessed in a thread-safe manner.
        /// </summary>
        public bool IsReserved { get; set; } = reserved;
    }

    private static readonly TimeSpan PollerPeriod = TimeSpan.FromSeconds(5);
    private readonly HttpClient _httpClient;
    private readonly PeriodicTimer _timer;
    private readonly Task _pollerTask;
    private readonly ILogger<WorkerPool> _logger;

    // Initialize workers with 0 available slots for safety, until the poller updates their actual slot count.
    private readonly ImmutableArray<Worker> _workers =
        ImmutableArray<Worker>.Empty
            .Add(new Worker(new Uri("http://localhost:5032"), 0, false))
            .Add(new Worker(new Uri("http://localhost:5033"), 0, false));

    public WorkerPool(ILogger<WorkerPool> logger)
    {
        _httpClient = new HttpClient();
        _timer = new PeriodicTimer(PollerPeriod);
        _logger = logger;
        _pollerTask = UpdateWorkersStatus();
    }

    public async Task<WorkerView?> ReserveWorker()
    {
        foreach (var worker in _workers)
        {
            using (await worker.Lock.LockAsync())
            {
                if (worker is { AvailableSlotCount: > 0, IsReserved: false })
                {
                    worker.IsReserved = true;
                    _logger.LogInformation("Worker {Uri} with available slots {AvailableSlotCount} was reserved", worker.Uri, worker.AvailableSlotCount);
                    return new WorkerView(worker.Uri, worker.AvailableSlotCount);
                }
            }
        }

        return null;
    }

    public async Task ReleaseWorker(Uri workerUri, uint newAvailableSlotCount)
    {
        foreach (var worker in _workers.Where(worker => worker.Uri.Equals(workerUri)))
        {
            using (await worker.Lock.LockAsync())
            {
                var availableSlotCount = worker.AvailableSlotCount;
                worker.AvailableSlotCount = newAvailableSlotCount;
                worker.IsReserved = false;
                _logger.LogInformation("Worker {Uri} with available slots {OldAvailableSlotCount} was released with new available slots count {NewAvailableSlotsCount}", worker.Uri, availableSlotCount, worker.AvailableSlotCount);
                return;
            }
        }

        // ReSharper disable once InconsistentlySynchronizedField, since logger is thread-safe
        _logger.LogWarning("Attempted to release unknown worker: {Uri}", workerUri);
    }

    public async ValueTask DisposeAsync()
    {
        _timer.Dispose();
        await _pollerTask;
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Actualizes the available slot count of each worker in the pool by polling their status endpoint.
    /// Actualization is required for workers that were previously declared unavailable (because job submit to them failed,
    /// or they recently joined the pool) and for those that have finished their jobs.
    /// </summary>
    private async Task UpdateWorkersStatus()
    {
        do
        {
            await Parallel.ForEachAsync(_workers, async (worker, cancellationToken) =>
            {
                using (await worker.Lock.LockAsync(cancellationToken))
                {
                    if (worker is { IsReserved: false })
                    {
                        var availableSlotCount = worker.AvailableSlotCount;
                        try
                        {
                            var response = await _httpClient.GetAsync(new Uri(worker.Uri, "/status"), cancellationToken);
                            if (response.IsSuccessStatusCode)
                            {
                                var statusResponse = await response.Content.ReadFromJsonAsync<StatusResponse>(cancellationToken: cancellationToken);
                                worker.AvailableSlotCount = statusResponse?.AvailableSlotCount ?? 0;
                            }
                            else
                            {
                                worker.AvailableSlotCount = 0;
                            }
                        }
                        catch (HttpRequestException)
                        {
                            worker.AvailableSlotCount = 0;
                        }

                        if (availableSlotCount != worker.AvailableSlotCount)
                        {
                            _logger.LogInformation("Poller changed worker {Uri} available slots from {OldAvailableSlotCount} to {NewAvailableSlotCount}", worker.Uri, availableSlotCount, worker.AvailableSlotCount);
                        }
                    }
                }
            });
        } while (await _timer.WaitForNextTickAsync());
    }
}
