using LoadBalancer;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpForwarder();

var app = builder.Build();
app.UseRouting();
app.MapForwarder("/{**catch-all}", "http://localhost:5032", ForwarderRequestConfig.Empty, new CustomTransformer());
app.Run();
