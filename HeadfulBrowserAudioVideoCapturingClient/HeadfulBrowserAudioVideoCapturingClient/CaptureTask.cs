using Newtonsoft.Json;

namespace HeadfulBrowserAudioVideoCapturingClient;

internal class CaptureTask
{
    [JsonProperty("urlOfWebPageToCapture")]
    internal string UrlOfWebPageToCapture { get; }

    [JsonProperty("webPageWidth")]
    internal int WebPageWidth { get; }

    [JsonProperty("webPageHeight")]
    internal int WebPageHeight { get; }

    [JsonProperty("mimeType")]
    internal string MimeType { get; }

    [JsonProperty("frameRate")]
    internal int FrameRate { get; }

    [JsonProperty("durationInSeconds")]
    internal int DurationInSeconds { get; }

    internal CaptureTask(string urlOfWebPageToCapture, int webPageWidth, int webPageHeight, string mimeType,
        int frameRate, int durationInSeconds)
    {
        UrlOfWebPageToCapture = urlOfWebPageToCapture;
        WebPageWidth = webPageWidth;
        WebPageHeight = webPageHeight;
        MimeType = mimeType;
        FrameRate = frameRate;
        DurationInSeconds = durationInSeconds;
    }
}
