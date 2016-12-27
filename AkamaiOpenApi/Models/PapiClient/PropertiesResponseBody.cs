using Newtonsoft.Json;

namespace AkamaiOpenApi.Models.PapiClient
{
    public class PropertiesResponse
    {
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public PropertyItem[] items { get; set; }
    }

    public class PropertyItem
    {
        public int latestVersion { get; set; }
        public int stagingVersion { get; set; }
        [JsonProperty("productionVersion", NullValueHandling = NullValueHandling.Ignore)]
        public int productionVersion { get; set; }
    }
}
