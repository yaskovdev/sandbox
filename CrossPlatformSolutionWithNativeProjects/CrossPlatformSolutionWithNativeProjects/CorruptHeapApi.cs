using System.Reflection;

namespace CrossPlatformSolutionWithNativeProjects;

using System.Runtime.InteropServices;

internal static class CorruptHeapApi
{
    [DllImport("NativeLibrary", EntryPoint = "corrupt_heap")]
    internal static extern void CorruptHeap();
    
    static CorruptHeapApi()
    {
        NativeLibrary.SetDllImportResolver(typeof(CorruptHeapApi).Assembly, (name, assembly, path) =>
        {
            if (name == "NativeLibrary")
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                    var libPath = Path.Combine(appDir, "libNativeLibrary.dylib");
                    return NativeLibrary.Load(libPath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
                    return NativeLibrary.Load("NativeLibrary.dll");
            }
            return IntPtr.Zero;
        });
    }
}
