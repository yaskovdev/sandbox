using AspNetSandbox;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataProcessingService, DataProcessingService>();
builder.Services.AddSingleton<ISingletonDependency, SingletonDependency>();
builder.Services.AddSingleton<ISocketHandlerFactory, SocketHandlerFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/sockets/{socketId:int}/subscribe",
        (int socketId, IDataProcessingService service) => service.StartProcessing(new SocketId(socketId)))
    .WithName("SubscribeToSocketWithId")
    .WithOpenApi();

app.MapPost("/sockets/{socketId:int}/unsubscribe",
        (int socketId, IDataProcessingService service) => service.StopProcessing(new SocketId(socketId)))
    .WithName("UnsubscribeFromSocketWithId")
    .WithOpenApi();
app.Run();
