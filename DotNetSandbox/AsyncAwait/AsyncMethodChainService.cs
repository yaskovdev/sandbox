using System;
using System.Threading.Tasks;

namespace AsyncAwait;

public class AsyncMethodChainService
{
    public void StartRecordingMode()
    {
        Console.WriteLine("Entered StartRecordingMode " + Environment.CurrentManagedThreadId);
        UserCommandTransactionSucceeded().ContinueWith(_ => { Console.WriteLine("Entered ContinueWith " + Environment.CurrentManagedThreadId); });
        Console.WriteLine("RecordingItemResume enqueued " + Environment.CurrentManagedThreadId);
    }

    private async Task UserCommandTransactionSucceeded()
    {
        Console.WriteLine("Entered UserCommandTransactionSucceeded " + Environment.CurrentManagedThreadId);
        await InitializeArtifactCache();
        Console.WriteLine("ArtifactCacheCreatedEvent enqueued " + Environment.CurrentManagedThreadId);
    }

    private async Task InitializeArtifactCache()
    {
        Console.WriteLine("Entered InitializeArtifactCache " + Environment.CurrentManagedThreadId);
        await Task.Run(() => SlowCalculation());
        // await Task.CompletedTask;
        // SlowCalculation();
        Console.WriteLine("Exited InitializeArtifactCache " + Environment.CurrentManagedThreadId);
    }

    private static void SlowCalculation()
    {
        Console.WriteLine("Entered SlowCalculation " + Environment.CurrentManagedThreadId);
        for (var i = 0; i < 100000000; i++)
        {
            var sin = Math.Sin(i);
        }
    }
}