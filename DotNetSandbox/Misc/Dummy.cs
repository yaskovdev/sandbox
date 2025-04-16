using Newtonsoft.Json;

namespace Misc;

public class Dummy
{
    [JsonProperty("InstanceUuid")]
    public string InstanceUuid { get; private set; } = "";

    [JsonProperty("Title")]
    public string Title { get; set; } = "";
}