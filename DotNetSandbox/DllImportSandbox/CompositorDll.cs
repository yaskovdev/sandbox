namespace DllImportSandbox;

using System.Runtime.InteropServices;

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
}
