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

    private readonly HttpClient _httpClient;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cancellationToken = new();
    private readonly Task _poller;
    private readonly ILogger<WorkerPool> _logger;

    // Initialize workers as busy for safety, until the poller updates their actual status.
    private readonly ImmutableArray<Worker> _workers =
        ImmutableArray<Worker>.Empty
            .Add(new Worker(new Uri("http://localhost:5032"), WorkerStatus.Busy))
            .Add(new Worker(new Uri("http://localhost:5033"), WorkerStatus.Busy));


    public WorkerPool(ILogger<WorkerPool> logger)
    {
        _httpClient = new HttpClient();
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        _logger = logger;
        _poller = UpdateWorkersStatus();
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
                    _logger.LogInformation("Reserved worker: {Uri}", worker.Uri);
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
                worker.Status = newStatus;
                _logger.LogInformation("Released worker: {Uri} with status: {Status}", worker.Uri, newStatus);
                return;
            }
        }

        // ReSharper disable once InconsistentlySynchronizedField, since logger is thread-safe
        _logger.LogWarning("Attempted to release unknown worker: {Uri}", workerUri);
    }

    private async Task UpdateWorkersStatus()
    {
        try
        {
            do
            {
                _logger.LogInformation("Updating workers status...");
                foreach (var worker in _workers)
                {
                    using (await worker.Lock.LockAsync())
                    {
                        if (worker.Status != WorkerStatus.Reserved)
                        {
                            try
                            {
                                var response = await _httpClient.GetAsync(new Uri(worker.Uri, "/health"));
                                worker.Status = response.IsSuccessStatusCode ? WorkerStatus.Idle : WorkerStatus.Busy;
                            }
                            catch (HttpRequestException)
                            {
                                worker.Status = WorkerStatus.Busy;
                            }
                        }
                    }
                }
            } while (await _timer.WaitForNextTickAsync(_cancellationToken.Token));
        }
        catch (OperationCanceledException)
        {
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationToken.CancelAsync();
        await _poller;
        _cancellationToken.Dispose();
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
