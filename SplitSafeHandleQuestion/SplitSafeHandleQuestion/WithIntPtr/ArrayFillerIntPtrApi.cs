namespace SplitSafeHandleQuestion.WithIntPtr;

using System.Runtime.InteropServices;

internal static class ArrayFillerIntPtrApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_two_arrays")]
    internal static extern int FillTwoArraysExtern(IntPtr xs, IntPtr ys, byte numberOfElementsInEachArray);
}
