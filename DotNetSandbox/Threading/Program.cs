namespace Threading;

internal static class Program
{
    private const int NumberOfDisposables = 100_000;

    public static void Main()
    {
        // TestDisposables();
        // new VolatileDemo().Run();
        // new Demo().Run();
        new ParticipantDemo().RunInSequence();
        new ParticipantDemo().RunInParallel();
    }

    private static void TestDisposables()
    {
        try
        {
            Test<SuperBadDisposable>();
            Console.WriteLine($"{nameof(SuperBadDisposable)} passed the test");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{nameof(SuperBadDisposable)} failed the test with exception: {e.Message}");
        }
        try
        {
            Test<JustBadDisposable>();
            Console.WriteLine($"{nameof(JustBadDisposable)} passed the test");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{nameof(JustBadDisposable)} failed the test with exception: {e.Message}");
        }
        try
        {
            Test<ThreadSafeDisposableWithSynchronization>();
            Console.WriteLine($"{nameof(ThreadSafeDisposableWithSynchronization)} passed the test");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{nameof(ThreadSafeDisposableWithSynchronization)} failed the test with exception: {e.Message}");
        }
        try
        {
            Test<ThreadSafeDisposableWithInterlocked>();
            Console.WriteLine($"{nameof(ThreadSafeDisposableWithInterlocked)} passed the test");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{nameof(ThreadSafeDisposableWithInterlocked)} failed the test with exception: {e.Message}");
        }
    }

    private static void Test<T>() where T : IDisposableWithCounter
    {
        var disposables = CreateDisposables<T>();
        var t1 = new Thread(() => DisposeDisposables(disposables));
        var t2 = new Thread(() => DisposeDisposables(disposables));
        Parallel.Invoke(t1.Start, t2.Start);
        t1.Join();
        t2.Join();
        for (var i = 0; i < NumberOfDisposables; i++)
        {
            if (disposables[i].Counter > 1)
            {
                throw new Exception($"The disposable {i} got disposed {disposables[i].Counter} times");
            }
        }
    }

    private static IList<T> CreateDisposables<T>() where T : IDisposableWithCounter
    {
        var disposables = new List<T>();
        for (var i = 0; i < NumberOfDisposables; i++)
        {
            var instance = Activator.CreateInstance(typeof(T)) ?? throw new Exception("Cannot create disposable instance");
            disposables.Add((T)instance);
        }
        return disposables;
    }

    private static void DisposeDisposables<T>(IList<T> disposables) where T : IDisposableWithCounter
    {
        for (var i = 0; i < NumberOfDisposables; i++)
        {
            disposables[i].Dispose();
        }
    }
}
