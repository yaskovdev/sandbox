# Video And Audio Demuxing With FFmpeg

The official FFmpeg demuxer and decoder example. The original code
is [here](https://github.com/FFmpeg/FFmpeg/blob/master/doc/examples/demux_decode.c).

The program prints instructions how to play the output after successful demuxing. You may want to adjust a video frame
rate by specifying, e.g., `-framerate 16`.

## Building And Running On Windows Using CLion

1. Download FFmpeg build for x64 to, say, `c:\dev\ffmpeg-master-latest-win64-gpl-shared`. Can go
   to `Windows builds by BtbN` from [the official page](https://ffmpeg.org/download.html#build-windows), then
   download an archive similar to `ffmpeg-N-112841-g2d9ed64859-win64-gpl-shared.zip`.
2. Add directory with FFmpeg DLL files (e.g., `C:\dev\ffmpeg-master-latest-win64-gpl-shared\bin`) to `Path`. Note:
   restart your IDE once you modify `Path`.
3. Go to `Build, Execution, Deployment -> CMake -> Debug -> Encironment`, create `CMAKE_PREFIX_PATH` variable and set
   it to `c:\dev\ffmpeg-master-latest-win64-gpl-shared\include;c:\dev\ffmpeg-master-latest-win64-gpl-shared\lib`.
4. Go to `Build, Execution, Deployment -> Toolchains -> Visual Studio -> Architecture` and make sure it is `amd64`.
5. Delete `cmake-build-debug` if it exists and run `Reload CMake Project`.

Use `t.mkv` or `capture.webm` as the input file.
