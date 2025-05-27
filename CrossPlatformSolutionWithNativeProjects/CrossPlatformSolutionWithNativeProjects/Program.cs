using System;

namespace CrossPlatformSolutionWithNativeProjects;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine("Going to simulate heap corruption");
        CorruptHeapApi.CorruptHeap();
    }
}
