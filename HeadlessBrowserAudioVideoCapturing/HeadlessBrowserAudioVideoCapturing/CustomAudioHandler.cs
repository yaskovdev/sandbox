using System.Reflection;
using System.Runtime.InteropServices;
using CefSharp;
using CefSharp.Handler;
using CefSharp.Structs;

namespace HeadlessBrowserAudioVideoCapturing;

public class CustomAudioHandler : AudioHandler
{
    private const bool ProceedWithAudioStreamCapture = true;

    private int numberOfChannels;

    private const int ChannelAlignment = 16;

    private readonly FileStream rawAudioFile;

    public CustomAudioHandler()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var executableLocation = Path.GetDirectoryName(assembly.Location);
        var dest = Path.Combine(executableLocation, "sound.ogg");
        this.rawAudioFile = new FileStream(dest, FileMode.Create, FileAccess.Write);
    }

    protected override bool GetAudioParameters(IWebBrowser chromiumWebBrowser, IBrowser browser, ref AudioParameters parameters)
    {
        // NumberOfChannels = parameters.ChannelLayout switch
        // {
        //     ChannelLayout.LayoutMono => 1,
        //     ChannelLayout.LayoutStereo => 2,
        //     _ => throw new ArgumentOutOfRangeException(nameof(parameters.ChannelLayout), parameters.ChannelLayout.ToString())
        // };
        return ProceedWithAudioStreamCapture;
    }

    protected override void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, AudioParameters parameters, int channels)
    {
        Console.WriteLine($"Audio stream started with channel layout {parameters.ChannelLayout}, number of channels {channels}");
        numberOfChannels = channels;
    }

    // each sample is 32 bit
    protected override void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr data, int noOfFrames, long pts)
    {
        unsafe
        {
            float** channelData = (float**)data.ToPointer();
            int size = numberOfChannels * noOfFrames * 4;
            byte[] samples = new byte[size];
            fixed (byte* pDestByte = samples)
            {
                float* pDest = (float*)pDestByte;

                for (int i = 0; i < noOfFrames; i++)
                {
                    for (int c = 0; c < numberOfChannels; c++)
                    {
                        *pDest++ = channelData[c][i];
                    }
                }
            }
            Console.WriteLine($"Successfully copied {size} bytes to managed array of length {samples.Length}");
            rawAudioFile.Write(samples, 0, size);
        }
        // Console.WriteLine($"Got audio packet with number of frames {noOfFrames}");
        // int[] pointers = new int[numberOfChannels];
        // Marshal.Copy(data, pointers, 0, numberOfChannels);
        // foreach (var pointer in pointers)
        // {
        //     var bufferPtr = new IntPtr(pointer);
        //     var buffer = new int[noOfFrames];
        //     Marshal.Copy(bufferPtr, buffer, 0, noOfFrames);
        // }
        // for (int i = 0; i < numberOfChannels; i++)
        // {
        //     var memorySizeInBytes = CalculateMemorySizeInBytes(numberOfChannels, noOfFrames);
        //     var buffer = new byte[memorySizeInBytes];
        //     Marshal.Copy(data, buffer, 0, memorySizeInBytes);
        //     
        // }
        // Console.WriteLine($"Successfully copied {memorySizeInBytes} to managed array");
    }

    private static int CalculateMemorySizeInBytes(int channels, int frames)
    {
        var alignedFrames = ((frames * sizeof(float) + ChannelAlignment - 1) & ~(ChannelAlignment - 1)) / sizeof(float);
        return sizeof(float) * channels * alignedFrames;
    }
}
