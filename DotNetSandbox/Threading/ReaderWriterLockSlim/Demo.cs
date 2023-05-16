namespace Threading.ReaderWriterLockSlim;

public class Demo
{
    public void Run()
    {
        var objectWithLock = new ObjectWithLock();

        var threadThatEntersLock = new Thread(() => { objectWithLock.Run(); });
        threadThatEntersLock.Start();
        Thread.Sleep(TimeSpan.FromSeconds(1));

        // var threadThatWaitsForLock = new Thread(() => { objectWithLock.Run(); });
        // threadThatWaitsForLock.Start();
        // Thread.Sleep(TimeSpan.FromSeconds(1));

        objectWithLock.Dispose();

        Thread.Sleep(TimeSpan.FromDays(1));
    }
}
