namespace Threading;

public class VolatileDemo
{
    internal void Run()
    {
        Task.Factory.StartNew(ResetFlagAfter1S);
        int x = 0;

        while (_flag)
            ++x;

        Console.WriteLine("Done");
    }

    private void ResetFlagAfter1S()
    {
        Thread.Sleep(1000);
        _flag = false;
    }

    private /* volatile */ bool _flag = true;
}
