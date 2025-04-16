using System.Collections.Immutable;

namespace Misc;

public class Util
{
    private record ItemWrapper(Item? Item);

    private record Item(uint Value);

    public static void DoStuff(uint value)
    {
        var items = ImmutableList.Create(new ItemWrapper(null), new ItemWrapper(new Item(1)));
        Console.WriteLine("Found item: " + items.FirstOrDefault(it => it.Item?.Value == value));
    }
}