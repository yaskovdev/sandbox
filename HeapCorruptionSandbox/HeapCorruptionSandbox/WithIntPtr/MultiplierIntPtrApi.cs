namespace DotNetCppSandbox.WithIntPtr;

using System.Runtime.InteropServices;

internal static class MultiplierIntPtrApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "create_multiplier")]
    internal static extern IntPtr CreateMultiplierExtern(int coefficient);

    [DllImport("NativeLibrary.dll", EntryPoint = "multiply")]
    internal static extern int MultiplyExtern(IntPtr multiplier, int number);

    [DllImport("NativeLibrary.dll", EntryPoint = "destroy_multiplier")]
    internal static extern IntPtr DestroyMultiplierExtern(IntPtr multiplier);
}
