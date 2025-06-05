using System.Diagnostics;
using System.Net;
using LoadBalancer;
using Yarp.ReverseProxy.Forwarder;

// TODO: check if a Service Bus queue with leases is a better solution for the load balancer.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWorkerPool, WorkerPool>();
builder.Services.AddHostedService<EagerInstantiationHostedService<IWorkerPool>>();
builder.Services.AddHttpForwarder();

var httpClient = new HttpMessageInvoker(new SocketsHttpHandler
{
    UseProxy = false,
    AllowAutoRedirect = false,
    AutomaticDecompression = DecompressionMethods.None,
    UseCookies = false,
    EnableMultipleHttp2Connections = true,
    ActivityHeadersPropagator = new ReverseProxyPropagator(DistributedContextPropagator.Current),
    ConnectTimeout = TimeSpan.FromSeconds(15)
});

var app = builder.Build();
app.UseRouting();
// Heavily inspired by https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/yarp/direct-forwarding?view=aspnetcore-9.0#example.
app.Map("/{**catch-all}", async (HttpContext httpContext, IHttpForwarder forwarder) =>
{
    var workerPool = app.Services.GetRequiredService<IWorkerPool>();
    var worker = await workerPool.ReserveWorker();
    if (worker == null)
    {
        // TODO: the existing implementation requires a free worker to even check if the job has already been submitted. Probably not a big deal, need to check.
        // One option is to use a busy worker to check if the job has already been submitted, or even check by yourself.
        return Results.StatusCode(503);
    }

    var error = await forwarder.SendAsync(httpContext, worker.WorkerUri.ToString(), httpClient);
    if (error == ForwarderError.None)
    {
        // If the worker responded with a 200 OK, it means the call already existed, therefore the session was not submitted,
        // so we release the worker with the same number of available slots.
        await workerPool.ReleaseWorker(worker.WorkerUri, httpContext.Response.StatusCode == StatusCodes.Status200OK ? worker.AvailableSlotCount : worker.AvailableSlotCount - 1);
    }
    else
    {
        // TODO: you should probably retry here, until the job is submitted. Or, likely better, the caller should retry.
        // TODO: you may want to use Idle if you are sure the job wasn't submitted to the worker
        // If we failed to submit the job, we assume that something went wrong with the worker, so we release it with 0 slots
        // to not try to submit other jobs to it until the poller in the pool actualizes its available slots later.
        await workerPool.ReleaseWorker(worker.WorkerUri, 0);
        var errorFeature = httpContext.GetForwarderErrorFeature();
        var exception = errorFeature?.Exception;
    }

    return Results.Empty;
});
app.Run();
