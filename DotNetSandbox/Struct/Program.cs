using System;

namespace Struct;

internal static class Program
{

    private static void Main()
    {
        var buffer = new Buffer();
        Console.WriteLine($"Default value of {nameof(buffer.Data)} is {buffer.Data}, its equality to {nameof(IntPtr.Zero)} is {buffer.Data == IntPtr.Zero}");
        Console.WriteLine($"Size of {nameof(IntPtr)} is {IntPtr.Size} bytes");
        new Service().PrintDuration();
        Console.WriteLine(TimeSpan.FromMilliseconds(2000).Seconds);
    }
}
