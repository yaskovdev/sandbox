namespace Misc;

using System.Collections.Immutable;

public class InterlockedSandbox
{
    private ImmutableArray<Box> unsorted = ImmutableArray<Box>.Empty.Add(new Box(2)).Add(new Box(1)).Add(new Box(3));
    private ImmutableArray<Box> sorted = ImmutableArray<Box>.Empty.Add(new Box(1)).Add(new Box(2)).Add(new Box(3));

    /// <summary>
    /// Does Sort for a sorted ImmutableArray return the old array or still a new one? Answer: the old array.
    /// </summary>
    public void Run()
    {
        Console.WriteLine("Sorted:");
        Console.WriteLine(ImmutableInterlocked.Update(ref sorted, array => array));
        Console.WriteLine(ImmutableInterlocked.Update(ref sorted, array => array.Sort(new BoxComparer())));
        Console.WriteLine(ImmutableInterlocked.Update(ref sorted, _ => ImmutableArray<Box>.Empty.Add(new Box(1)).Add(new Box(2)).Add(new Box(3))));
        Console.WriteLine("Unsorted:");
        Console.WriteLine(ImmutableInterlocked.Update(ref unsorted, array => array));
        Console.WriteLine(ImmutableInterlocked.Update(ref unsorted, array => array.Sort(new BoxComparer())));
        Console.WriteLine(ImmutableInterlocked.Update(ref unsorted, _ => ImmutableArray<Box>.Empty.Add(new Box(1)).Add(new Box(2)).Add(new Box(3))));
    }
}
