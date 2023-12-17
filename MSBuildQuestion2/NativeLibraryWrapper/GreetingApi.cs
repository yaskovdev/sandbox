namespace NativeLibraryWrapper;

using System.Runtime.InteropServices;

public static class GreetingApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "greet")]
    public static extern void Greet();
}
