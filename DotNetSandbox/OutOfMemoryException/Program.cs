namespace OutOfMemoryException;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Wait for about a minute for the {nameof(OutOfMemoryException)} to happen");
        var list = new List<int>();
        for (var i = 0; i < int.MaxValue; i++)
        {
            list.Add(i);
        }
    }
}
