# Heap Corruption Sandbox

Install [Windows SDK](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/).

Be sure to pick the `Release` configuration for the build, then build the sandbox.

```powershell
# You can omit the `/full` flag, then standard heap verification will be enabled
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /enable HeapCorruptionSandbox.exe /full
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p
.\HeapCorruptionSandbox\bin\x64\Release\net6.0\HeapCorruptionSandbox.exe; $LastExitCode
```

To disable the heap verification, run the following command:

```powershell
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /disable HeapCorruptionSandbox.exe
```

More details on heap verification can be found in the [documentation](https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/gflags-and-pageheap).

## Important Notes

In order for the program to crash immediately on heap corruption, you should write past the end of the heap allocation.

The allocation size
is [8 bytes in 32-bit systems](https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/cpp/libraries/pageheap-detect-memory-errors)
and [16 bytes in 64-bit systems](https://devblogs.microsoft.com/oldnewthing/20170410-00/?p=95935).

That's why the `size` in the `corrupt_heap` is 16 bytes.

To make it crash independently on the value of the `size`, you can try passing the `/unaligned` flag (see the above article), but most likely your program won't
work at all.

## Running in Docker

```powershell
docker build -f HeapCorruptionSandbox/Windows.Dockerfile -t yaskovdev/heap-corruption-sandbox .
docker run -d yaskovdev/heap-corruption-sandbox
```