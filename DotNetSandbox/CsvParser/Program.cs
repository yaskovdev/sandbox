namespace CsvParser;

using System.Globalization;
using CsvHelper;

internal static class Program
{
    private static void Main()
    {
        using var reader = new StreamReader(@"c:/dev/Logs_2023_03_07_10_11.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Foo>().ToList();
        for (int i = 1; i < records.Count; i++)
        {
            var diff = Parse(records[i].PreciseTimeStamp) - Parse(records[i - 1].PreciseTimeStamp);
            if (diff.TotalSeconds >= 3)
            {
                Console.WriteLine(diff);
            }
        }
    }

    private static DateTime Parse(string source) => DateTime.Parse(source, CultureInfo.InvariantCulture);
}
