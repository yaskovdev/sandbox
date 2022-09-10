namespace SplitSafeHandleQuestion.WithSafeHandle;

using System.Runtime.InteropServices;

public class FrameProducerSafeHandleApi
{
    public int FillChromaAndLuma(ArraySafeHandle chroma, ArraySafeHandle luma, byte numberOfBytesInEachPlane) =>
        FillChromaAndLumaExtern(chroma, luma, numberOfBytesInEachPlane);

    [DllImport("NativeLibrary.dll", EntryPoint = "fill_chroma_and_luma")]
    private static extern int FillChromaAndLumaExtern(ArraySafeHandle chroma, ArraySafeHandle luma, byte numberOfBytesInEachPlane);
}
