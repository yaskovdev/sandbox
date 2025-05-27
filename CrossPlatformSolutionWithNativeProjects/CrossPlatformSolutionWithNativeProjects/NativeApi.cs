using System.Reflection;

namespace CrossPlatformSolutionWithNativeProjects;

using System.Runtime.InteropServices;

internal static class NativeApi
{
    private const string NativeLibraryName = "NativeLibrary";

    [DllImport(NativeLibraryName, EntryPoint = "hello_world")]
    internal static extern void HelloWorld();
    
    static NativeApi()
    {
        NativeLibrary.SetDllImportResolver(typeof(NativeApi).Assembly, (name, assembly, path) =>
        {
            if (name == NativeLibraryName)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                    var libPath = Path.Combine(appDir, $"lib{NativeLibraryName}.dylib");
                    return NativeLibrary.Load(libPath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
                    return NativeLibrary.Load($"{NativeLibraryName}.dll");
            }
            return IntPtr.Zero;
        });
    }
}
