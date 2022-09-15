namespace Threading;

public interface IDisposableWithCounter : IDisposable
{
    int Counter { get; }
}
