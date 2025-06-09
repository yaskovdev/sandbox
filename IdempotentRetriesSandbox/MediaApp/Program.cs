using MediaApp;
using StackExchange.Redis;

const uint totalSlotCount = 3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IConnectionMultiplexer>(_ => new FaultyConnectionMultiplexerDecorator(ConnectionMultiplexer.Connect("localhost")));
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<ISessionFactory, SessionFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/status",
        (ISessionService service) => new MediaAppStatusResponse(totalSlotCount - service.SessionCount))
    .WithName("Status")
    .WithOpenApi();

// TODO: it should respond with 2** if created the call and session in Redis successfully. Otherwise there will be a retry rejected as duplicate, which does not makes sense.
// TODO: can it be that the endpoint is called, the session is not yet added to the _sessions dictionary, and then the status is called that returns 0?
// In that case, the instance may be submitted more sessions than it can handle.
app.MapPut("/calls/{callId}",
        (string callId, ISessionService service) =>
        {
            var outcome = service.CreateCall(callId);
            var response = new CallResponse(callId);
            return outcome == Outcome.Created ? Results.Created($"/calls/{callId}", response) : Results.Ok(response);
        })
    .WithName("CreateCall")
    .WithOpenApi();

app.MapPost("/calls/{callId}/sessions/{sessionId}/transfer",
        (string callId, string sessionId, ISessionService service) =>
        {
            var newSessionId = service.TransferSession(callId, sessionId);
            return Results.Created($"/calls/{callId}/sessions/{newSessionId}", new SessionResponse(callId, sessionId));
        })
    .WithName("TransferSession")
    .WithOpenApi();

app.Run();
