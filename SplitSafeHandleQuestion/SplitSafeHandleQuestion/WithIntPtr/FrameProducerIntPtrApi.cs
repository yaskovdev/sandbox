namespace SplitSafeHandleQuestion.WithIntPtr;

using System.Runtime.InteropServices;

internal static class FrameProducerIntPtrApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "fill_chroma_and_luma")]
    internal static extern int FillChromaAndLuma(IntPtr chroma, IntPtr luma, byte numberOfBytesInEachPlane);
}
