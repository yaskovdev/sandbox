namespace DllImportSandbox;

using System.Runtime.InteropServices;
using System.Security;

public static class CompositorDll
{
    public enum CompositorType
    {
        COMPOSITOR_TYPE_FIXED = 0,
        COMPOSITOR_TYPE_GRID = 1,
        COMPOSITOR_TYPE_JSON = 2
    }

    public enum LogLevel
    {
        Error = 0,
        Warning = 1,
        Info = 2
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
    public struct Source
    {
        public int id;
        public long timestamp;
        // NV12
        public IntPtr imageLuma;
        public IntPtr imageChroma;
        public int imageWidth;
        public int imageHeight;
        public int imageLumaStride;
        public int imageChromaStride;
        // Alpha
        public IntPtr alphaData;
        public int alphaWidth;
        public int alphaHeight;
        public int alphaStride;

        public string displayName;
        public int domSpk;
        public int muted;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CompositionResult
    {
        public int type;
        public int code;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void LogFunction(LogLevel logLevel, string message, IntPtr userData);

    [DllImport("Microsoft.SlimCV.dll")]
    public static extern IntPtr CreateCompositor(CompositorType type, LogFunction callback, IntPtr userData);

    [DllImport("Microsoft.SlimCV.dll", CharSet = CharSet.Ansi)]
    public static extern IntPtr CreateCompositorDescription(string json);

    [DllImport("Microsoft.SlimCV.dll")]
    public static extern void CompositorSetDescription(IntPtr handle, IntPtr desc);
    
    [DllImport("Microsoft.SlimCV.dll")]
    public static extern void DestroyCompositorDescription(IntPtr handle);

    [DllImport("Microsoft.SlimCV.dll")]
    public static extern void DestroyCompositor(IntPtr handle);
    
    [SuppressUnmanagedCodeSecurity]
    [DllImport("Microsoft.SlimCV.dll", SetLastError = false)]
    public static extern CompositionResult CompositorComposeFrames(IntPtr handle, long tsMillis, [In] Source[] sources, int numSources, IntPtr dstLuma, IntPtr dstChroma, int dstWidth, int dstHeight, int dstStrideLuma, int dstStrideChroma);
}
