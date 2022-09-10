namespace SplitSafeHandleQuestion.WithSafeHandle;

using System.Runtime.InteropServices;

public class ArrayFillerSafeHandleApi
{
    public int FillTwoArrays(ArraySafeHandle xs, ArraySafeHandle ys, byte numberOfElementsInEachArray) =>
        FillTwoArraysExtern(xs, ys, numberOfElementsInEachArray);

    [DllImport("NativeLibrary.dll", EntryPoint = "fill_two_arrays")]
    private static extern int FillTwoArraysExtern(ArraySafeHandle xs, ArraySafeHandle ys, byte numberOfElementsInEachArray);
}
