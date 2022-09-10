namespace SplitSafeHandleQuestion.WithSafeHandle;

using System.Runtime.InteropServices;

public class FrameProducerSafeHandleApi
{
    public int FillChromaAndLuma(ArraySafeHandle chroma, ArraySafeHandle luma, byte numberOfElementsInEachPlane) =>
        FillChromaAndLumaExtern(chroma, luma, numberOfElementsInEachPlane);

    [DllImport("NativeLibrary.dll", EntryPoint = "fill_chroma_and_luma")]
    private static extern int FillChromaAndLumaExtern(ArraySafeHandle xs, ArraySafeHandle ys, byte numberOfElementsInEachPlane);
}
