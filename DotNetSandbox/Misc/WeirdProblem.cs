namespace Misc;

public class WeirdProblem
{
    public void Swap(int x, int y)
    {
        x *= y;
        y = x / y;
        x /= y;
        Console.WriteLine($"({x}, {y})");
    }
}
