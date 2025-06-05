namespace LoadBalancer;

using System.Collections.Immutable;
using Ardalis.GuardClauses;
using NeoSmart.AsyncLock;

public class WorkerPool : IWorkerPool, IAsyncDisposable
{
    private class Worker(Uri uri, WorkerStatus status)
    {
        public AsyncLock Lock { get; } = new();

        public Uri Uri { get; } = uri;

        // The property should be accessed in a thread-safe manner.
        public WorkerStatus Status { get; set; } = status;
    }

    private static readonly TimeSpan PollerPeriod = TimeSpan.FromSeconds(5);
    private readonly HttpClient _httpClient;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cancellationToken = new();
    private readonly Task _pollerTask;
    private readonly ILogger<WorkerPool> _logger;

    // Initialize workers as busy for safety, until the poller updates their actual status.
    private readonly ImmutableArray<Worker> _workers =
        ImmutableArray<Worker>.Empty
            .Add(new Worker(new Uri("http://localhost:5032"), WorkerStatus.Busy))
            .Add(new Worker(new Uri("http://localhost:5033"), WorkerStatus.Busy));

    public WorkerPool(ILogger<WorkerPool> logger)
    {
        _httpClient = new HttpClient();
        _timer = new PeriodicTimer(PollerPeriod);
        _logger = logger;
        _pollerTask = UpdateWorkersStatus();
    }

    public async Task<Uri?> ReserveWorker()
    {
        foreach (var worker in _workers)
        {
            using (await worker.Lock.LockAsync())
            {
                if (worker.Status == WorkerStatus.Idle)
                {
                    worker.Status = WorkerStatus.Reserved;
                    LogWorkerStatusChange(worker.Uri, WorkerStatus.Idle, worker.Status);
                    return worker.Uri;
                }
            }
        }

        return null;
    }

    public async Task ReleaseWorker(Uri workerUri, WorkerStatus newStatus)
    {
        Guard.Against.InvalidInput(newStatus, nameof(newStatus), c => c is WorkerStatus.Idle or WorkerStatus.Busy, $"New status must be either {WorkerStatus.Idle} or {WorkerStatus.Busy}");

        foreach (var worker in _workers.Where(worker => worker.Uri.Equals(workerUri)))
        {
            using (await worker.Lock.LockAsync())
            {
                var status = worker.Status;
                worker.Status = newStatus;
                LogWorkerStatusChange(worker.Uri, status, worker.Status);
                return;
            }
        }

        // ReSharper disable once InconsistentlySynchronizedField, since logger is thread-safe
        _logger.LogWarning("Attempted to release unknown worker: {Uri}", workerUri);
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationToken.CancelAsync();
        await _pollerTask;
        _cancellationToken.Dispose();
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task UpdateWorkersStatus()
    {
        try
        {
            do
            {
                // TODO: probably faster to run it in parallel
                foreach (var worker in _workers)
                {
                    using (await worker.Lock.LockAsync())
                    {
                        // TODO: you should probably poll the busy workers less often.
                        if (worker.Status is WorkerStatus.Idle or WorkerStatus.Busy)
                        {
                            var status = worker.Status;
                            try
                            {
                                var response = await _httpClient.GetAsync(new Uri(worker.Uri, "/status"));
                                if (response.IsSuccessStatusCode)
                                {
                                    var statusResponse = await response.Content.ReadFromJsonAsync<StatusResponse>();
                                    worker.Status = statusResponse?.SessionCount == 0 ? WorkerStatus.Idle : WorkerStatus.Busy;
                                }
                                else
                                {
                                    worker.Status = WorkerStatus.Busy;
                                }
                            }
                            catch (HttpRequestException)
                            {
                                worker.Status = WorkerStatus.Busy;
                            }

                            LogWorkerStatusChange(worker.Uri, status, worker.Status);
                        }
                    }
                }
            } while (await _timer.WaitForNextTickAsync(_cancellationToken.Token));
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void LogWorkerStatusChange(Uri workerUri, WorkerStatus oldStatus, WorkerStatus newStatus)
    {
        if (oldStatus != newStatus)
        {
            _logger.LogInformation("Worker {Uri} status changed from {OldStatus} to {NewStatus}", workerUri, oldStatus, newStatus);
        }
    }
}
