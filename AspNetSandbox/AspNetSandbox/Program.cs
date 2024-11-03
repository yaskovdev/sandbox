using AspNetSandbox;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStatelessService, StatelessService>();
builder.Services.AddSingleton<ISingletonDependency, SingletonDependency>();
builder.Services.AddSingleton<IStatefulServiceFactory, StatefulServiceFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/sockets/{socketId:int}/process",
        (int socketId, IStatelessService service) => service.Process(new SocketId(socketId)))
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();