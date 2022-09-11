namespace SplitSafeHandleQuestion.WithSafeHandle;

using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// ReSharper disable once ClassNeverInstantiated.Global
public class ArraySafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private GCHandle _handle; // GCHandle is a struct, must not be readonly

    public ArraySafeHandle(byte[] array) : base(true)
    {
        _handle = GCHandle.Alloc(array, GCHandleType.Pinned);
        SetHandle(_handle.AddrOfPinnedObject());
    }

    protected override bool ReleaseHandle()
    {
        _handle.Free();
        return true;
    }
}
