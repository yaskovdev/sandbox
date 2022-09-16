namespace PixelConverter;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var bytes = File.ReadAllBytes(@"c:\dev\tasks\2981883\212.raw");
        Console.WriteLine(bytes.Length);
    }
}
