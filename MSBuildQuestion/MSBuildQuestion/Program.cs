namespace MSBuildQuestion;

using System.Runtime.InteropServices;

internal static class Program
{
    public static void Main() => GreetingApi.Greet();
}

internal static class GreetingApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "greet")]
    internal static extern void Greet();
}
