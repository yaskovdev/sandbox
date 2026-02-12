namespace CsvParser;

using System.Globalization;
using CsvHelper;

internal static class Program
{
    private static void Main()
    {
        using var reader =
            new StreamReader(@"c:\dev\composer_measurements_with_fix.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<CompositionMeasurements>().ToList();
        Console.WriteLine("Total samples: " + records.Count);
        Console.WriteLine("Average Duration: " + TimeSpan.FromMilliseconds(Average(records.Select(r => r.Duration.TotalMilliseconds).ToArray())));
        Console.WriteLine("Duration StdDev: " + TimeSpan.FromMilliseconds(StdDev(Variance(records.Select(r => r.Duration.TotalMilliseconds).ToArray()))));
        Console.WriteLine("Average TotalProcessorTime: " + TimeSpan.FromMilliseconds(Average(records.Select(r => r.TotalProcessorTime.TotalMilliseconds).ToArray())));
        Console.WriteLine("TotalProcessorTime StdDev: " + TimeSpan.FromMilliseconds(StdDev(Variance(records.Select(r => r.TotalProcessorTime.TotalMilliseconds).ToArray()))));
    }

    private static double Sum(double[] values) => values.Sum();

    private static double Average(double[] values)
    {
        var sum = Sum(values);
        return sum / values.Length;
    }

    private static double Variance(double[] values)
    {
        if (values.Length == 0) return 0.0;
        var avg = Average(values);
        var variance = values.Sum(value => Math.Pow(value - avg, 2.0));
        return variance / values.Length;
    }

    private static double StdDev(double var) => Math.Sqrt(var);
}