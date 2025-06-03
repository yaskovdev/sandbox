using MediaApp;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect("localhost"));
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<ISessionFactory, SessionFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPut("/calls/{callId}",
        (string callId, ISessionService service) => service.CreateCall(callId))
    .WithName("CreateCall")
    .WithOpenApi();

app.MapPost("/calls/{callId}/sessions/{sessionId}/transfer",
        (string callId, string sessionId, ISessionService service) => service.TransferSession(callId, sessionId))
    .WithName("TransferSession")
    .WithOpenApi();

app.MapDelete("/sessions/{sessionId}",
        (string sessionId, ISessionService service) => service.DeleteSession(sessionId))
    .WithName("DeleteSession")
    .WithOpenApi();

app.Run();
