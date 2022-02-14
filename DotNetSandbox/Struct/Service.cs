namespace Struct;

public class Service
{
    private long totalDurationInTicks;

    public void PrintDuration()
    {
        Console.WriteLine($"Duration is {totalDurationInTicks} ticks");
    }
}
