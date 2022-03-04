using System.Text;
using Newtonsoft.Json;

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
            var request = new HttpRequestMessage(HttpMethod.Post, CapturingServerBaseAddress);
            var task = new CaptureTask("http://localhost:8000", 800, 600);
            request.Content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            await using var clientStream = await response.Content.ReadAsStreamAsync();
            await using var fs = new FileStream($"{link}.webm", FileMode.Create);
            await clientStream.CopyToAsync(fs);
        });

        await Task.WhenAll(tasks);
        Console.WriteLine("All done");
    }
}
