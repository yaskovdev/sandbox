To build and run using CLion on macOS, make sure that:

1. File -> Reload CMake Project
2. Run -> Edit Configurations -> Program Arguments -> /Users/yaskovdev/dev/output.mp4
3. `^R`

To build and run using CLion on Windows, make sure that:

1. You have ffmpeg build for x64 located in, say, `c:\dev\ffmpeg`.
2. `c:\dev\ffmpeg\bin\` is in your `Path`.
3. `Build, Execution, Deployment -> CMake -> Debug -> Environment` has `CMAKE_PREFIX_PATH` variable set
   to `C:\dev\ffmpeg\include;C:\dev\ffmpeg\lib`.
4. `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` is `amd64`. Also make
   sure `Visual Studio` is 1st in the list.
5. Pass as "Program arguments" file name with input packet and file name to write the output frame to,
   e.g., `c:\dev\output.mp4`.
