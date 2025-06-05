namespace LoadBalancer;

using System.Collections.Immutable;
using NeoSmart.AsyncLock;

public class WorkerPool : IWorkerPool, IAsyncDisposable
{
    private class Worker(Uri uri, bool reserved, int availableSlotCount)
    {
        public AsyncLock Lock { get; } = new();

        public Uri Uri { get; } = uri;

        /// <summary>
        /// Indicates that a worker is temporarily reserved. In this state, the worker is neither available 
        /// for new tasks nor can have its status updated by the poller.
        /// The property should be accessed in a thread-safe manner.
        /// </summary>
        public bool IsReserved { get; set; } = reserved;

        // The property should be accessed in a thread-safe manner.
        public int AvailableSlotCount { get; set; } = availableSlotCount;
    }

    private static readonly TimeSpan PollerPeriod = TimeSpan.FromSeconds(5);
    private readonly HttpClient _httpClient;
    private readonly PeriodicTimer _timer;
    private readonly Task _pollerTask;
    private readonly ILogger<WorkerPool> _logger;

    // Initialize workers with 0 available slots for safety, until the poller updates their actual slot count.
    private readonly ImmutableArray<Worker> _workers =
        ImmutableArray<Worker>.Empty
            .Add(new Worker(new Uri("http://localhost:5032"), false, 0))
            .Add(new Worker(new Uri("http://localhost:5033"), false, 0));

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
                if (worker is { IsReserved: false, AvailableSlotCount: > 0 })
                {
                    worker.IsReserved = true;
                    _logger.LogInformation("Worker {Uri} with available slots {AvailableSlotCount} was reserved", worker.Uri, worker.AvailableSlotCount);
                    return new WorkerView(worker.Uri, worker.AvailableSlotCount);
                }
            }
        }

        return null;
    }

    public async Task ReleaseWorker(Uri workerUri, int newAvailableSlotCount)
    {
        foreach (var worker in _workers.Where(worker => worker.Uri.Equals(workerUri)))
        {
            using (await worker.Lock.LockAsync())
            {
                var availableSlotCount = worker.AvailableSlotCount;
                worker.IsReserved = false;
                worker.AvailableSlotCount = newAvailableSlotCount;
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

    private async Task UpdateWorkersStatus()
    {
        do
        {
            // TODO: probably faster to run it in parallel
            foreach (var worker in _workers)
            {
                using (await worker.Lock.LockAsync())
                {
                    // TODO: you should probably only poll workers that were previously declared unavailable because you failed to submit a job to them
                    // or because they recently joined the pool (though joining the pool is not implemented currently).
                    if (worker is { IsReserved: false })
                    {
                        var availableSlotCount = worker.AvailableSlotCount;
                        try
                        {
                            var response = await _httpClient.GetAsync(new Uri(worker.Uri, "/status"));
                            if (response.IsSuccessStatusCode)
                            {
                                var statusResponse = await response.Content.ReadFromJsonAsync<StatusResponse>();
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
            }
        } while (await _timer.WaitForNextTickAsync());
    }
}
