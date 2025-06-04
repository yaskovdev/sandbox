namespace LoadBalancer;

using System.Collections.Immutable;
using Ardalis.GuardClauses;

public class WorkerPool : IWorkerPool, IDisposable
{
    private class Worker(Uri uri, WorkerStatus status)
    {
        public Uri Uri { get; } = uri;

        // The property should be accessed in a thread-safe manner.
        public WorkerStatus Status { get; set; } = status;
    }

    private readonly HttpClient _httpClient;
    private readonly Timer _timer;
    private readonly ILogger<WorkerPool> _logger;

    // Initialize workers as busy for safety, until the poller updates their actual status.
    private readonly ImmutableArray<Worker> _workers =
        ImmutableArray<Worker>.Empty
            .Add(new Worker(new Uri("http://localhost:5032"), WorkerStatus.Busy))
            .Add(new Worker(new Uri("http://localhost:5033"), WorkerStatus.Busy));


    public WorkerPool(ILogger<WorkerPool> logger)
    {
        _httpClient = new HttpClient();
        _timer = new Timer(UpdateWorkersStatus, _workers, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        _logger = logger;
    }

    public Uri? ReserveWorker()
    {
        foreach (var worker in _workers)
        {
            lock (worker)
            {
                if (worker.Status == WorkerStatus.Idle)
                {
                    worker.Status = WorkerStatus.Reserved;
                    _logger.LogInformation("Reserved worker: {Uri}", worker.Uri);
                    return worker.Uri;
                }
            }
        }

        return null;
    }

    public void ReleaseWorker(Uri workerUri, WorkerStatus newStatus)
    {
        Guard.Against.InvalidInput(newStatus, nameof(newStatus), c => c is WorkerStatus.Idle or WorkerStatus.Busy, $"New status must be either {WorkerStatus.Idle} or {WorkerStatus.Busy}");

        foreach (var worker in _workers)
        {
            if (worker.Uri.Equals(workerUri))
            {
                lock (worker)
                {
                    worker.Status = newStatus;
                    _logger.LogInformation("Released worker: {Uri} with status: {Status}", worker.Uri, newStatus);
                    return;
                }
            }
        }

        // ReSharper disable once InconsistentlySynchronizedField, since logger is thread-safe
        _logger.LogWarning("Attempted to release unknown worker: {Uri}", workerUri);
    }

    private void UpdateWorkersStatus(object? state)
    {
        var workers = (ImmutableArray<Worker>)state;
        foreach (var worker in workers)
        {
            lock (worker)
            {
                if (worker.Status != WorkerStatus.Reserved)
                {
                    try
                    {
                        var response = _httpClient.GetAsync(new Uri(worker.Uri, "/health")).GetAwaiter().GetResult();
                        worker.Status = response.IsSuccessStatusCode ? WorkerStatus.Idle : WorkerStatus.Busy;
                    }
                    catch (HttpRequestException)
                    {
                        worker.Status = WorkerStatus.Busy;
                    }
                }
            }
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _timer.Dispose();
        GC.SuppressFinalize(this);
    }
}
