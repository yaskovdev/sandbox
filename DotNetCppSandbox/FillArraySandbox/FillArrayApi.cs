namespace FillArraySandbox;

using System.Runtime.InteropServices;

internal static class FillArrayApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_array")]
    internal static extern void FillArray(byte[] array, int length);
}
