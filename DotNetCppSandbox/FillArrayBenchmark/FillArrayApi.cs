namespace FillArrayBenchmark;

using System.Runtime.InteropServices;

internal static class FillArrayApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_array", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FillArray(byte[] array, int length);

    /// <summary>
    /// Note: if you are changing the position of the <c>size</c> param, make sure to update the <c>SizeParamIndex</c>. 
    /// </summary>
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_array", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FillArrayFixed([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] array, int size);
}
