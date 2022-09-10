namespace SplitSafeHandleQuestion.WithByteArrays;

using System.Runtime.InteropServices;

internal static class FrameProducerIntArrayApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_chroma_and_luma")]
    internal static extern int FillChromaAndLuma(byte[] xs, byte[] ys, byte numberOfElementsInEachArray);
}
