namespace Misc;

public class Frame : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Frame disposed");
    }
}
