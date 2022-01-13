In order to build and run on Windows using CLion, make sure that:

1. You have ffmpeg build for x64 located in, say, `c:\dev\ffmpeg`.
2. `Build, Execution, Deployment -> CMake -> Debug -> Encironment` has `CMAKE_PREFIX_PATH` variable set to `C:\dev\ffmpeg\include;C:\dev\ffmpeg\lib`.
3. Directory with ffmpeg DLL files (e.g., `C:\dev\ffmpeg\bin`) is in `Path`. Note: restart of IDE may be needed once you modify Path.
4. `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` is `amd64`.
