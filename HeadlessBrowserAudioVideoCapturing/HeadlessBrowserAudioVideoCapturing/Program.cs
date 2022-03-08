using CefSharp;
using CefSharp.DevTools.Page;
using CefSharp.OffScreen;
using Nito.AsyncEx;

namespace HeadlessBrowserAudioVideoCapturing;

public static class Program
{
    public static void Main()
    {
        AsyncContext.Run(async delegate
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            settings.EnableAudio();
            Cef.Initialize(settings);
            using var browser = new ChromiumWebBrowser("https://yaskovdev.github.io/video-and-audio-capturing-test/hello-delayed-5-seconds.html");
            browser.AudioHandler = new CustomAudioHandler();
            var initialLoadResponse = await browser.WaitForInitialLoadAsync();
            AssertSuccess(initialLoadResponse.Success,
                $"Page load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
            var devToolsClient = browser.GetDevToolsClient();
            var page = devToolsClient.Page;
            page.ScreencastFrame += async (_, args) =>
            {
                await page.ScreencastFrameAckAsync(args.SessionId);
                // Console.WriteLine($"Got frame with session ID {args.SessionId} and timestamp {args.Metadata.Timestamp}");
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "frames", $"capture_{DateTime.UtcNow.Ticks}.png");
                await File.WriteAllBytesAsync(path, args.Data);
            };
            var response = await page.StartScreencastAsync(StartScreencastFormat.Png, 100, 800, 600, 1);
            AssertSuccess(response.Success, $"Cannot start screencast, DevTools response is {response.ResponseAsJsonString}");
            Console.ReadLine();
        });
    }

    private static void AssertSuccess(bool success, string errorMessage)
    {
        if (!success) throw new Exception(errorMessage);
    }
}
