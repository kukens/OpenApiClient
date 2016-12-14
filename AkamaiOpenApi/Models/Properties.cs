using Newtonsoft.Json;

namespace AkamaiOpenApi.Models
{
    public class Properties
    {
        public Property properties { get; set; }
    }

    public class Property
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
