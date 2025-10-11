using System.Diagnostics;

namespace StreamingSandbox;

public class StopwatchWrapper : IStopwatch
{
    private readonly Stopwatch _stopwatch = new();

    public TimeSpan Elapsed => _stopwatch.Elapsed;

    public void Start()
    {
        _stopwatch.Start();
    }

    public void Restart()
    {
        _stopwatch.Restart();
    }
}