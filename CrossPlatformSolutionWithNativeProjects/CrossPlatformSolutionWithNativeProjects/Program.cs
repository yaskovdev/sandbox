namespace CrossPlatformSolutionWithNativeProjects;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine("Going to call a native function");
        NativeApi.HelloWorld();
    }
}
