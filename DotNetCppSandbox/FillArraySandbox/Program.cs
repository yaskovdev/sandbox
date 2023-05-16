namespace FillArraySandbox;

internal static class Program
{
    public static void Main()
    {
        var array = new byte[] { 0, 0, 0, 0, 0 };
        Console.WriteLine("Array before filling: " + string.Join(", ", array));
        FillArrayApi.FillArray(array, array.Length);
        Console.WriteLine("Array after filling: " + string.Join(", ", array));
    }
}
