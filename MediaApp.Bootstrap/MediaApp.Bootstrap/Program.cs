using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.R9.Extensions.Diagnostics;

namespace MediaApp.Bootstrap;

public static class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureServices(Configure)
            .UseWindowsUtilizationTracker()
            .Build();
        var task = host.RunAsync();
        Console.WriteLine("Main is about to wait and then return");
        Thread.Sleep(10000);
        Console.WriteLine("Status: " + task.Status);
    }

    private static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<WorkerService>();
        services.Configure<HostOptions>(x => x.ShutdownTimeout = TimeSpan.FromSeconds(30));
    }
}
