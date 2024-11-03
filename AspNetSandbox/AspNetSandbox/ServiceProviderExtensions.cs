namespace AspNetSandbox;

public static class ServiceProviderExtensions
{
    public static T GetService<T>(this IServiceProvider serviceProvider, params object[] parameters) =>
        ActivatorUtilities.CreateInstance<T>(serviceProvider, parameters);
}