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

    internal CaptureTask(string urlOfWebPageToCapture, int webPageWidth, int webPageHeight)
    {
        UrlOfWebPageToCapture = urlOfWebPageToCapture;
        WebPageWidth = webPageWidth;
        WebPageHeight = webPageHeight;
    }
}
