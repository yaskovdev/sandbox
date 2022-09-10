namespace SplitSafeHandleQuestion;

using System.Runtime.InteropServices;
using WithByteArrays;
using WithIntPtr;
using WithSafeHandle;

internal static class Program
{
    public static void Main()
    {
        UseIntPtr();
        UseByteArray();
        UseSafeHandle();
    }

    private static void UseIntPtr()
    {
        Console.WriteLine("With IntPtr:");
        const byte length = 10;
        var ptr = Marshal.AllocHGlobal(2 * length);
        try
        {
            var status = ArrayFillerIntPtrApi.FillTwoArraysExtern(ptr, ptr + length, length);
            ProcessArray(ptr, 2 * length);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    private static void UseByteArray()
    {
        Console.WriteLine("With byte[]:");
        const byte length = 10;
        var xs = new byte[length];
        var ys = new byte[length];
        var status = ArrayFillerIntArrayApi.FillTwoArraysExtern(xs, ys, length);
        var array = new byte[2 * length];

        // Requires copying:
        Array.Copy(xs, 0, array, 0, length);
        Array.Copy(ys, 0, array, length, length);
        var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
        try
        {
            ProcessArray(handle.AddrOfPinnedObject(), 2 * length);
        }
        finally
        {
            handle.Free();
        }
    }

    private static void UseSafeHandle()
    {
        Console.WriteLine("With SafeHandle:");
        var api = new ArrayFillerSafeHandleApi();
        const byte length = 10;
        var xs = new byte[length];
        var ys = new byte[length];
        using (var xsHandle = new ArraySafeHandle(xs))
        {
            using (var ysHandle = new ArraySafeHandle(ys))
            {
                api.FillTwoArrays(xsHandle, ysHandle, length);
            }
        }
        var array = new byte[2 * length];
        // Requires copying:
        Array.Copy(xs, 0, array, 0, length);
        Array.Copy(ys, 0, array, length, length);
        var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
        try
        {
            ProcessArray(handle.AddrOfPinnedObject(), 2 * length);
        }
        finally
        {
            handle.Free();
        }
    }

    private static void ProcessArray(IntPtr ptr, int length)
    {
        Console.Write("Going to process array: ");
        for (var i = 0; i < length; ++i)
        {
            if (i > 0)
            {
                Console.Write(", ");
            }
            Console.Write(Marshal.ReadByte(ptr + i));
        }
        Console.WriteLine();
    }
}
