namespace SplitSafeHandleQuestion.WithByteArrays;

using System.Runtime.InteropServices;

internal static class ArrayFillerIntArrayApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_two_arrays")]
    internal static extern int FillTwoArraysExtern(byte[] xs, byte[] ys, byte numberOfElementsInEachArray);
}
