namespace HeapCorruptionSandbox;

using System.Runtime.InteropServices;

internal static class CorruptHeapApi
{
    [DllImport("NativeLibrary.dll", EntryPoint = "corrupt_heap")]
    internal static extern void CorruptHeap();
}
