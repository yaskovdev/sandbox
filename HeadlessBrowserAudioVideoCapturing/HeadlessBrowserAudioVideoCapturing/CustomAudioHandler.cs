using CefSharp;
using CefSharp.Structs;

namespace HeadlessBrowserAudioVideoCapturing;

public class CustomAudioHandler : IAudioHandler
{
    public void Dispose()
    {
        Console.WriteLine(nameof(Dispose));
    }

    public bool GetAudioParameters(IWebBrowser chromiumWebBrowser, IBrowser browser, ref AudioParameters parameters)
    {
        Console.WriteLine(nameof(GetAudioParameters));
        return true;
    }

    public void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, AudioParameters parameters,
        int channels)
    {
        Console.WriteLine(nameof(OnAudioStreamStarted));
    }

    public void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr data, int noOfFrames,
        long pts)
    {
        Console.WriteLine(nameof(OnAudioStreamPacket));
    }

    public void OnAudioStreamStopped(IWebBrowser chromiumWebBrowser, IBrowser browser)
    {
        Console.WriteLine(nameof(OnAudioStreamStopped));
    }

    public void OnAudioStreamError(IWebBrowser chromiumWebBrowser, IBrowser browser, string errorMessage)
    {
        Console.WriteLine(nameof(OnAudioStreamError));
    }
}
