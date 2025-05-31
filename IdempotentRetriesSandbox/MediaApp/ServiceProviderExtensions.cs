namespace MediaApp;

public static class ServiceProviderExtensions
{
    public static T CreateInstance<T>(this IServiceProvider serviceProvider, params object[] parameters) =>
        ActivatorUtilities.CreateInstance<T>(serviceProvider, parameters);
}
