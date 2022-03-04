using CefSharp;
using CefSharp.Handler;
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
        using var browser = new ChromiumWebBrowser("http://localhost:8000/");
        browser.AudioHandler = new CustomAudioHandler();
        var initialLoadResponse = await browser.WaitForInitialLoadAsync();
        if (!initialLoadResponse.Success)
        {
            throw new Exception(
                $"Page load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
        }

        await Task.Delay(500);
        browser.ExecuteScriptAsync("document.getElementById('btn').click();");
        var bitmapAsByteArray = await browser.CaptureScreenshotAsync();
        var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "CefSharp_screenshot.png");
        Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);
        await File.WriteAllBytesAsync(screenshotPath, bitmapAsByteArray);
        Console.ReadLine();
    }
}
