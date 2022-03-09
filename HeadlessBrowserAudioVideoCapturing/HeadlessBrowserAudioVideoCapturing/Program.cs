using CefSharp;
using CefSharp.DevTools.Page;
using CefSharp.OffScreen;
using Nito.AsyncEx;

namespace HeadlessBrowserAudioVideoCapturing;

public static class Program
{
    private static readonly string WorkingDirectory;

    static Program()
    {
        WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "media");
    }

    public static void Main()
    {
        AsyncContext.Run(async delegate
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            settings.EnableAudio();
            Cef.Initialize(settings);
            using var browser = new ChromiumWebBrowser("https://www.youtube.com/embed/WPTxkU38BKg?autoplay=1");
            browser.AudioHandler = new CustomAudioHandler(WorkingDirectory);
            var initialLoadResponse = await browser.WaitForInitialLoadAsync();
            AssertSuccess(initialLoadResponse.Success,
                $"Page load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
            var devToolsClient = browser.GetDevToolsClient();
            var page = devToolsClient.Page;
            page.ScreencastFrame += async (_, args) =>
            {
                await page.ScreencastFrameAckAsync(args.SessionId);
                var path = Path.Combine(WorkingDirectory, $"frame_{DateTime.UtcNow.Ticks}.png");
                await File.WriteAllBytesAsync(path, args.Data);
            };
            var response = await page.StartScreencastAsync(StartScreencastFormat.Png);
            AssertSuccess(response.Success, $"Cannot start screencast, DevTools response is {response.ResponseAsJsonString}");
            Console.ReadLine();
        });
    }

    private static void AssertSuccess(bool success, string errorMessage)
    {
        if (!success) throw new Exception(errorMessage);
    }
}
