namespace Threading.ReaderWriterLockSlim;

using ReaderWriterLockSlim = System.Threading.ReaderWriterLockSlim;

public class ObjectWithLock : IDisposable
{
    private readonly ReaderWriterLockSlim _lockObject = new();

    public void Run()
    {
        Console.WriteLine($"{nameof(ObjectWithLock)} lock requested");
        _lockObject.EnterWriteLock();
        Console.WriteLine($"{nameof(ObjectWithLock)} lock entered");
        try
        {
            Thread.Sleep(TimeSpan.FromDays(1));
        }
        finally
        {
            _lockObject.ExitWriteLock();
            Console.WriteLine($"{nameof(ObjectWithLock)} lock exited");
        }
    }

    public void Dispose()
    {
        _lockObject.Dispose();
        Console.WriteLine($"{nameof(ObjectWithLock)} disposed");
    }
}
