To build and run using CLion on Windows, make sure that:

1. You have ffmpeg build for x64 located in, say, `c:\dev\ffmpeg`.
2. `c:\dev\ffmpeg\bin\` is in your `Path`.
3. `Build, Execution, Deployment -> CMake -> Debug -> Environment` has `CMAKE_PREFIX_PATH` variable set
   to `C:\dev\ffmpeg\include;C:\dev\ffmpeg\lib`.
4. `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` is `amd64`. Also make sure `Visual Studio` is 1st in the list.
5. Pass as "Program arguments" file name with input packet and file name to write the output frame to,
   e.g., `c:\dev\212.encoded c:\dev\212.raw`.

CMAKE_PREFIX_PATH=c:\dev\tools\FFmpeg.Stable.6.1.0\build\native\include\;c:\dev\tools\FFmpeg.Stable.6.1.0\build\native\lib\x64\