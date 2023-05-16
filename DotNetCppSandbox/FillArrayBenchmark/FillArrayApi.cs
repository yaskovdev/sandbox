namespace FillArrayBenchmark;

using System.Runtime.InteropServices;

internal static class FillArrayApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_array", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FillArray(byte[] array, int length);

    [DllImport("NativeLibrary.dll", EntryPoint = "fill_array", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FillArrayFixed([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] array, int length);
}
