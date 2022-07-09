using Polly;

namespace HttpClientWithRetry;

// Borrowed from https://stackoverflow.com/a/35183487/1862286.
public class HttpRetryMessageHandler : DelegatingHandler
{
    public HttpRetryMessageHandler(HttpMessageHandler handler) : base(handler)
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
        Policy
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .OrResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() => base.SendAsync(request, cancellationToken));
}
