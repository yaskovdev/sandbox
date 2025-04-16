# Heap Corruption Sandbox

Install [Windows SDK](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/).

Be sure to pick the `Release` configuration for the build, then build the sandbox.

```powershell
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /enable HeapCorruptionSandbox.exe /full
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p
.\HeapCorruptionSandbox\bin\x64\Release\net6.0\HeapCorruptionSandbox.exe; $LastExitCode
```

To disable the heap verification, run the following command:

```powershell
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /disable HeapCorruptionSandbox.exe
```
