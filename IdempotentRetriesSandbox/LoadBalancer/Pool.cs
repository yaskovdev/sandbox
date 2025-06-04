namespace LoadBalancer;

using System.Collections.Immutable;

public class Pool : IPool
{
    private readonly ILogger<Pool> _logger;

    private readonly ImmutableArray<Uri> _workers =
        ImmutableArray<Uri>.Empty
            .Add(new Uri("http://localhost:5032"))
            .Add(new Uri("http://localhost:5033"));

    private readonly HttpClient _httpClient;
    private readonly Timer _timer;
    private ImmutableHashSet<Uri> _availableWorkers = ImmutableHashSet<Uri>.Empty;

    public Pool(ILogger<Pool> logger)
    {
        _httpClient = new HttpClient();
        _timer = new Timer(UpdateAvailableWorkersPool, _workers, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        _logger = logger;
    }

    private void UpdateAvailableWorkersPool(object? state)
    {
        var workers = (ImmutableArray<Uri>)state;
        var availableWorkers = new HashSet<Uri>();
        foreach (var worker in workers)
        {
            _httpClient.GetAsync(new Uri(worker, "/health"));
            try
            {
                var response = _httpClient.GetAsync(new Uri(worker, "/health")).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    availableWorkers.Add(worker);
                }
            }
            catch (HttpRequestException)
            {
            }
        }

        Interlocked.Exchange(ref _availableWorkers, availableWorkers.ToImmutableHashSet());
        _logger.LogInformation("Number of available workers is {Count}", _availableWorkers.Count);
    }

    public Uri? GetAvailableWorker() => _availableWorkers.FirstOrDefault();
}
