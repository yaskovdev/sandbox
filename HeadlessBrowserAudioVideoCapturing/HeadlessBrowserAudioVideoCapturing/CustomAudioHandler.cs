using CefSharp;
using CefSharp.Handler;
using CefSharp.Structs;

namespace HeadlessBrowserAudioVideoCapturing;

public class CustomAudioHandler : AudioHandler
{
    private const bool ProceedWithAudioStreamCapture = true;

    protected override bool GetAudioParameters(IWebBrowser chromiumWebBrowser, IBrowser browser, ref AudioParameters parameters)
    {
        return ProceedWithAudioStreamCapture;
    }

    protected override void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, AudioParameters parameters, int channels)
    {
        Console.WriteLine($"Audio stream started with channel layout {parameters.ChannelLayout}");
    }

    protected override void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr data, int noOfFrames, long pts)
    {
        // Console.WriteLine($"Got audio packet with number of frames {noOfFrames}");
    }
}
