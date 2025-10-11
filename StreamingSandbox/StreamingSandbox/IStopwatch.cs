namespace StreamingSandbox;

public interface IStopwatch
{
    TimeSpan Elapsed { get; }
    void Start();
    void Restart();
}