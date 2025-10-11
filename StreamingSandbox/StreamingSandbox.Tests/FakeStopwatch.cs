namespace StreamingSandbox.Tests;

public class FakeStopwatch : IStopwatch
{
    public TimeSpan Elapsed { get; set; }

    public void Start()
    {
    }

    public void Restart()
    {
    }
}