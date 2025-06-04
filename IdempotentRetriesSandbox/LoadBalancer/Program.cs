using LoadBalancer;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPool, Pool>();
builder.Services.AddSingleton<CustomTransformer>();
builder.Services.AddHttpForwarder();

var app = builder.Build();
app.UseRouting();
app.MapForwarder("/{**catch-all}", "http://localhost:5032", ForwarderRequestConfig.Empty, app.Services.GetRequiredService<CustomTransformer>());
app.Run();
