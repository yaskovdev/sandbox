namespace FfmpegSandbox;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        using var process = new FfmpegProcess("4001");
        var stream = process.Start();
        var bytes = File.ReadAllBytes("sample.mp4");
        while (true)
        {
            Console.WriteLine($"Going to write {bytes.Length / (double)1024:0.00} KB of media");
            stream.Write(bytes);
            Thread.Sleep(1000);
        }
    }
}
