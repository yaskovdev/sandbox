using CefSharp;
using CefSharp.DevTools.Page;
using CefSharp.OffScreen;

namespace HeadlessBrowserAudioVideoCapturing;

public static class Program
{
    public static async Task Main()
    {
        var settings = new CefSettings();
        settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
        settings.EnableAudio();
        Cef.Initialize(settings);
        using var browser = new ChromiumWebBrowser("https://yaskovdev.github.io/video-and-audio-capturing-test/");
        browser.AudioHandler = new CustomAudioHandler();
        var initialLoadResponse = await browser.WaitForInitialLoadAsync();
        if (!initialLoadResponse.Success)
        {
            throw new Exception($"Page load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
        }
        await Task.Delay(1000);
        var devToolsClient = browser.GetDevToolsClient();
        var pageClient = devToolsClient.Page;
        var response = await pageClient.StartScreencastAsync(StartScreencastFormat.Jpeg, 100, 800, 600, 1);
        Console.WriteLine($"Response is {response.Success}");
        pageClient.ScreencastFrame += async (sender, args) =>
        {
            await pageClient.ScreencastFrameAckAsync(args.SessionId);
            Console.WriteLine($"Got frame with session ID {args.SessionId}");
        };
        var captureScreenshotResponse = await pageClient.CaptureScreenshotAsync();
        var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "CefSharp_screenshot.png");
        Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);
        await File.WriteAllBytesAsync(screenshotPath, captureScreenshotResponse.Data);

        Console.ReadLine();
    }
}
