using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediaApp.Bootstrap;

public static class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureServices(Configure)
            .Build();
        host.Run();
    }

    private static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<WorkerService>();
    }
}
