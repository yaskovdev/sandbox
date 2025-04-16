namespace DotNetCppSandbox.WithSafeHandle;

using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// ReSharper disable once ClassNeverInstantiated.Global
public class MultiplierSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public MultiplierSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        DestroyMultiplierExtern(handle);
        return true;
    }

    [DllImport("NativeLibrary.dll", EntryPoint = "destroy_multiplier")]
    private static extern IntPtr DestroyMultiplierExtern(IntPtr multiplier);
}
