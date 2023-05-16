namespace FfmpegSandbox;

using System.ComponentModel;
using System.Diagnostics;

public sealed class FfmpegProcess : IDisposable
{
    private const int WaitForExitMs = 5000;

    private const string InputArgs = "-re -i -";

    private readonly string _srtOutputPort;
    private string OutputArgs => $"-c copy -f mpegts \"srt://0.0.0.0:{_srtOutputPort}?mode=listener\"";

    private readonly Process _process;

    public FfmpegProcess(string srtOutputPort)
    {
        _srtOutputPort = srtOutputPort;
        _process = new Process
        {
            StartInfo =
            {
                FileName = "ffmpeg",
                Arguments = $"{InputArgs} {OutputArgs}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true
            }
        };
    }

    public Stream Start()
    {
        _process.Start();
        return _process.StandardInput.BaseStream;
    }

    public void Dispose()
    {
        Console.WriteLine("Waiting for the ffmpeg process to exit for {0} ms", WaitForExitMs);
        var exited = _process.WaitForExit(WaitForExitMs);
        if (!exited)
        {
            KillFfmpegProcessSafe();
        }
    }

    private void KillFfmpegProcessSafe()
    {
        Console.WriteLine("The ffmpeg process did not exit after {0} ms, killing the process", WaitForExitMs);
        try
        {
            _process.Kill();
        }
        catch (Win32Exception)
        {
            // ignored
        }
    }
}
