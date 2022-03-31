using System.Text;
using Newtonsoft.Json;

namespace HeadfulBrowserAudioVideoCapturingClient;

public static class Program
{
    private static readonly Uri CapturingServerBaseAddress =
        // new("http://localhost:8080/captures");
        new("http://capturing-server-ingress.c977159d4da548cfab16.westeurope.aksapp.io/captures");

    public static async Task Main()
    {
        var links = new[] { 0, 1, 2 };
        var httpClient = new HttpClient();
        const int capturingDurationInSeconds = 60;
        httpClient.BaseAddress = CapturingServerBaseAddress;
        httpClient.Timeout = TimeSpan.FromSeconds(capturingDurationInSeconds + 30);

        var tasks = links.Select(async link =>
        {
            Console.WriteLine($"Sending task {link}");
            var request = new HttpRequestMessage(HttpMethod.Post, CapturingServerBaseAddress);
            // const string mimeType = @"video/x-matroska";
            const string mimeType = @"video/webm";
            // var outputFileExtension = mimeType.Split("/")[1];
            var outputFileExtension = "webm";
            var task = new CaptureTask("https://yaskovdev.github.io/video-and-audio-capturing-test/", 1920, 1080,
                mimeType, 15, capturingDurationInSeconds);
            request.Content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            await using var capturedContent = await response.Content.ReadAsStreamAsync();
            await using var file = new FileStream($"{link}.{outputFileExtension}", FileMode.Create);
            await capturedContent.CopyToAsync(file);
        });

        await Task.WhenAll(tasks);
        Console.WriteLine("All done");
    }
}
