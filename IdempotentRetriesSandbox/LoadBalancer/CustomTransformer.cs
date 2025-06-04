namespace LoadBalancer;

using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

// TODO: check if a Service Bus queue with leases is a better solution for the load balancer.
// Heavily inspired by https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/yarp/direct-forwarding?view=aspnetcore-9.0#example.
internal sealed class CustomTransformer(IPool pool) : HttpTransformer
{
    public override async ValueTask TransformRequestAsync(HttpContext httpContext, HttpRequestMessage proxyRequest, string destinationPrefix, CancellationToken cancellationToken)
    {
        await base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix, cancellationToken);

        var availableWorker = pool.GetAvailableWorker();
        if (availableWorker == null)
        {
            throw new InvalidOperationException("No available workers in the pool");
        }

        proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(availableWorker.ToString(), httpContext.Request.Path, new QueryTransformContext(httpContext.Request).QueryString);
        proxyRequest.Headers.Host = null;
    }
}
