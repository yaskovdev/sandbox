namespace Misc;

public class Lena
{
    public Func<int, int, int> Sum3(int a)
    {
        //Func<int, int, int> SumRest = (x, y) => x + y;
        return (x, y) => a + x + y;
    }

    public void Run()
    {
        var sum2 = Sum3(1);
        sum2(2, 3); // == 6
    }
}
