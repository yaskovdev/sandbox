using System.Reflection;
using CefSharp;
using CefSharp.Handler;
using CefSharp.Structs;

namespace HeadlessBrowserAudioVideoCapturing;

public class CustomAudioHandler : AudioHandler
{
    private const bool ProceedWithAudioStreamCapture = true;

    private int _numberOfChannels;

    private readonly FileStream _rawAudioFile;

    public CustomAudioHandler()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyPath = assembly.Location;
        var executableLocation = Path.GetDirectoryName(assemblyPath) ?? throw new Exception($"Cannot find location of assembly {assemblyPath}");
        var dest = Path.Combine(executableLocation, $"sound_{DateTime.UtcNow.Ticks}.pcm");
        _rawAudioFile = new FileStream(dest, FileMode.Create, FileAccess.Write);
    }

    protected override bool GetAudioParameters(IWebBrowser chromiumWebBrowser, IBrowser browser, ref AudioParameters parameters) =>
        ProceedWithAudioStreamCapture;

    protected override void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, AudioParameters parameters, int channels)
    {
        Console.WriteLine($"Audio stream started with channel layout {parameters.ChannelLayout}, number of channels {channels}");
        _numberOfChannels = channels;
    }

    protected override void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr data, int numberOfFramesPerChannel, long pts)
    {
        unsafe
        {
            var channelData = (float**)data.ToPointer();
            var size = _numberOfChannels * numberOfFramesPerChannel * 4;
            var samples = new byte[size];
            fixed (byte* destByte = samples)
            {
                var dest = (float*)destByte;
                for (var i = 0; i < numberOfFramesPerChannel; i++)
                {
                    for (var c = 0; c < _numberOfChannels; c++)
                    {
                        *dest++ = channelData[c][i];
                    }
                }
            }
            Console.WriteLine($"Successfully copied {samples.Length} bytes to managed array");
            _rawAudioFile.Write(samples, 0, samples.Length);
        }
    }
}
