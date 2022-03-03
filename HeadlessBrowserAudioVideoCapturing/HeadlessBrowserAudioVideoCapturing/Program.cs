using CefSharp.OffScreen;

namespace HeadlessBrowserAudioVideoCapturing;

public static class Program
{
    public static async Task Main()
    {
        using var browser = new ChromiumWebBrowser("https://www.youtube.com/watch?v=tPEE9ZwTmy0");
        var initialLoadResponse = await browser.WaitForInitialLoadAsync();
        if (!initialLoadResponse.Success)
        {
            throw new Exception($"Page load failed with ErrorCode:{initialLoadResponse.ErrorCode}, HttpStatusCode:{initialLoadResponse.HttpStatusCode}");
        }
        await Task.Delay(500);
        var bitmapAsByteArray = await browser.CaptureScreenshotAsync();
        var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CefSharp_screenshot.png");
        Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);
    }
}