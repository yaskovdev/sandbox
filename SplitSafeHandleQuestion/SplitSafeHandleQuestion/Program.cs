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
        const byte resultingFrameLength = 6;
        var resultingFrame = new byte[resultingFrameLength];
        var resultingFrameHandle = GCHandle.Alloc(resultingFrame, GCHandleType.Pinned);
        var resultingFramePtr = resultingFrameHandle.AddrOfPinnedObject();
        try
        {
            var chromaPtr = resultingFramePtr;
            var lumaPtr = resultingFramePtr + resultingFrameLength / 2;
            var status = FrameProducerIntPtrApi.FillChromaAndLuma(chromaPtr, lumaPtr, resultingFrameLength / 2);
            DisplayFrame(resultingFramePtr, resultingFrameLength);
        }
        finally
        {
            resultingFrameHandle.Free();
        }
    }

    private static void UseByteArray()
    {
        Console.WriteLine("With byte[]:");
        const byte resultingFrameLength = 6;
        var chroma = new byte[resultingFrameLength / 2];
        var luma = new byte[resultingFrameLength / 2];
        var status = FrameProducerIntArrayApi.FillChromaAndLuma(chroma, luma, resultingFrameLength / 2);
        var resultingFrame = new byte[resultingFrameLength];

        // Requires copying:
        Array.Copy(chroma, 0, resultingFrame, 0, resultingFrameLength / 2);
        Array.Copy(luma, 0, resultingFrame, resultingFrameLength / 2, resultingFrameLength / 2);
        var handle = GCHandle.Alloc(resultingFrame, GCHandleType.Pinned);
        try
        {
            DisplayFrame(handle.AddrOfPinnedObject(), resultingFrameLength);
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
        const byte resultingFrameLength = 6;
        var chroma = new byte[resultingFrameLength / 2];
        var luma = new byte[resultingFrameLength / 2];
        using (var chromaHandle = new ArraySafeHandle(chroma))
        {
            using (var lumaHandle = new ArraySafeHandle(luma))
            {
                api.FillChromaAndLuma(chromaHandle, lumaHandle, resultingFrameLength / 2);
            }
        }
        var resultingFrame = new byte[resultingFrameLength];

        // Requires copying:
        Array.Copy(chroma, 0, resultingFrame, 0, resultingFrameLength / 2);
        Array.Copy(luma, 0, resultingFrame, resultingFrameLength / 2, resultingFrameLength / 2);
        var resultingFrameHandle = GCHandle.Alloc(resultingFrame, GCHandleType.Pinned);
        try
        {
            DisplayFrame(resultingFrameHandle.AddrOfPinnedObject(), resultingFrameLength);
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
