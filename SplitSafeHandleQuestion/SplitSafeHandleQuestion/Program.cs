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
        var resultingFramePtr = Marshal.AllocHGlobal(2 * length);
        try
        {
            var status = FrameProducerIntPtrApi.FillChromaAndLuma(resultingFramePtr, resultingFramePtr + length, length);
            DisplayFrame(resultingFramePtr, 2 * length);
        }
        finally
        {
            Marshal.FreeHGlobal(resultingFramePtr);
        }
    }

    private static void UseByteArray()
    {
        Console.WriteLine("With byte[]:");
        const byte length = 10;
        var chroma = new byte[length];
        var luma = new byte[length];
        var status = FrameProducerIntArrayApi.FillChromaAndLuma(chroma, luma, length);
        var resultingFrame = new byte[2 * length];

        // Requires copying:
        Array.Copy(chroma, 0, resultingFrame, 0, length);
        Array.Copy(luma, 0, resultingFrame, length, length);
        var handle = GCHandle.Alloc(resultingFrame, GCHandleType.Pinned);
        try
        {
            DisplayFrame(handle.AddrOfPinnedObject(), 2 * length);
        }
        finally
        {
            handle.Free();
        }
    }

    private static void UseSafeHandle()
    {
        Console.WriteLine("With SafeHandle:");
        var api = new FrameProducerSafeHandleApi();
        const byte length = 10;
        var chroma = new byte[length];
        var luma = new byte[length];
        using (var chromaHandle = new ArraySafeHandle(chroma))
        {
            using (var lumaHandle = new ArraySafeHandle(luma))
            {
                api.FillChromaAndLuma(chromaHandle, lumaHandle, length);
            }
        }
        var resultingFrame = new byte[2 * length];

        // Requires copying:
        Array.Copy(chroma, 0, resultingFrame, 0, length);
        Array.Copy(luma, 0, resultingFrame, length, length);
        var resultingFrameHandle = GCHandle.Alloc(resultingFrame, GCHandleType.Pinned);
        try
        {
            DisplayFrame(resultingFrameHandle.AddrOfPinnedObject(), 2 * length);
        }
        finally
        {
            resultingFrameHandle.Free();
        }
    }

    private static void DisplayFrame(IntPtr framePtr, int length)
    {
        Console.Write("Going to display frame: ");
        // Just printing the frame bytes for simplicity:
        for (var i = 0; i < length; ++i)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(Marshal.ReadByte(framePtr + i));
        }
        Console.WriteLine();
    }
}
