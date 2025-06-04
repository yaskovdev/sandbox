namespace LoadBalancer;

using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

// TODO: check if a Service Bus queue with leases is a better solution for the load balancer.
// Heavily inspired by https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/yarp/direct-forwarding?view=aspnetcore-9.0#example.
internal sealed class CustomTransformer : HttpTransformer
{
    public override async ValueTask TransformRequestAsync(HttpContext httpContext, HttpRequestMessage proxyRequest, string destinationPrefix, CancellationToken cancellationToken)
    {
        await base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix, cancellationToken);

        var queryContext = new QueryTransformContext(httpContext.Request);
        queryContext.Collection.Remove("param1");
        queryContext.Collection["area"] = "xx2";

        proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress("http://localhost:5032", httpContext.Request.Path, queryContext.QueryString);

        proxyRequest.Headers.Host = null;
    }
}
