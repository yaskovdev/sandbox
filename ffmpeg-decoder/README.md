To build and run on Windows, make sure that:

1. You have ffmpeg build for x64 located in, say, `c:\dev\ffmpeg`.
2. `c:\dev\ffmpeg\bin\` should be added in your `Path`.
3. `Build, Execution, Deployment -> CMake -> Debug -> Encironment` has `CMAKE_PREFIX_PATH` variable set to `C:\dev\ffmpeg\include;C:\dev\ffmpeg\lib`.
4. `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` is `amd64`.