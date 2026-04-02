namespace InterlockedVisibilityDemo;

// Based on this example: https://stackoverflow.com/a/24556591.
// The program is expected to print "Done" after approximately 1 second. Actually, it will get stuck.
// Run the Release config to reproduce.
internal class Program
{
    private int _flag;

    private void Run()
    {
        Task.Run(ResetFlagAfter1S);
        var x = 0;

        // To fix, replace "_flag" with "Interlocked.CompareExchange(ref _flag, 0, 0)"
        while (_flag == 0)
            ++x;

        Console.WriteLine("Done");
    }

    private void ResetFlagAfter1S()
    {
        Thread.Sleep(1000);
        Interlocked.Exchange(ref _flag, 1);
    }

    private static void Main()
    {
        new Program().Run();
    }
}