# Cross Platform Solution with Native Projects

Install [Windows SDK](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/).

Be sure to pick the `Release` configuration for the build, then build the sandbox.

```powershell
# You can omit the `/full` flag, then standard heap verification will be enabled
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /enable CrossPlatformSolutionWithNativeProjects.exe /full
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p
.\CrossPlatformSolutionWithNativeProjects\bin\x64\Release\net9.0\CrossPlatformSolutionWithNativeProjects.exe; $LastExitCode
```

To disable the heap verification, run the following command:

```powershell
& "c:\Program Files (x86)\Windows Kits\10\Debuggers\x64\gflags.exe" /p /disable CrossPlatformSolutionWithNativeProjects.exe
```

More details on heap verification can be found in the [documentation](https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/gflags-and-pageheap).

## Building in macOS

Install .NET SDK and CMake if not already installed:

```bash
brew install --cask dotnet-sdk
brew install cmake
```

Then, build the native library and the .NET project:

```bash
mkdir ./NativeLibrary/build
cd ./NativeLibrary/build
cmake ..
make

cd ../../
dotnet build CrossPlatformSolutionWithNativeProjects/CrossPlatformSolutionWithNativeProjects.csproj
dotnet run --project CrossPlatformSolutionWithNativeProjects/CrossPlatformSolutionWithNativeProjects.csproj
```

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
docker build -f CrossPlatformSolutionWithNativeProjects/Windows.Dockerfile -t yaskovdev/crossplatformsolutionwithnativeprojects .
docker run -d yaskovdev/crossplatformsolutionwithnativeprojects
```

Note: installing the Build Tools (`vs_buildtools.exe`) may take a while, so be patient. It will appear as if the
`vs_buildtools.exe` is downloaded, but then the installation got stuck. Just wait a bit longer and it will finish
installing.
