using System.Web;

namespace HeadfulBrowserAudioVideoCapturingClient;

public static class Program
{
    private static readonly Uri CapturingServerBaseAddress = new("http://127.0.0.1:3000/");

    public static async Task Main()
    {
        var links = new[] { 0 };
        var httpClient = new HttpClient();
        httpClient.BaseAddress = CapturingServerBaseAddress;

        var tasks = links.Select(async link =>
        {
            var webPageToCapture = HttpUtility.UrlEncode("http://localhost:8000");
            await using var clientStream = await httpClient.GetStreamAsync($"?link={webPageToCapture}");
            await using var fs = new FileStream($"{link}.webm", FileMode.Create);
            await clientStream.CopyToAsync(fs);
        });

        await Task.WhenAll(tasks);
        Console.WriteLine("All done");
    }
}
