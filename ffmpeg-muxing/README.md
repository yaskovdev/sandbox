NOTE: the project was temporarily replaced with the muxer example
from https://github.com/FFmpeg/FFmpeg/blob/master/doc/examples/mux.c. Revert the latest commit to have the old muxer.

Run `ffmpeg -listen 1 -timeout 10000 -f flv -loglevel debug -i rtmp://127.0.0.1:5000/mystream/test -vcodec libx264 -y test_command_line.mp4`
before running the app.

To build and run using CLion on macOS, make sure that:

1. File -> Reload CMake Project
2. Run -> Edit Configurations -> Program Arguments -> rtmp://127.0.0.1:5000/mystream/test
3. `^R`

To build and run using CLion on Windows, make sure that:

1. You have ffmpeg build for x64 located in, say, `c:\dev\ffmpeg`.
2. `c:\dev\ffmpeg\bin\` is in your `Path`.
3. `Build, Execution, Deployment -> CMake -> Debug -> Environment` has `CMAKE_PREFIX_PATH` variable set
   to `C:\dev\ffmpeg\include;C:\dev\ffmpeg\lib`.
4. `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` is `amd64`. Also make
   sure `Visual Studio` is 1st in the list.
5. Pass as "Program arguments" file name with input packet and file name to write the output frame to,
   e.g., `rtmp://127.0.0.1:5000/mystream/test`.
