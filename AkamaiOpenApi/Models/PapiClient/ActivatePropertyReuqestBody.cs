using Newtonsoft.Json;

namespace AkamaiOpenApi.Models.PapiClient
{
    public class ActivatePropertyReuqestBody
    {
        public int propertyVersion { get; set; }
        public string network { get; set; }
        public string note { get; set; }
        public string[] notifyEmails { get; set; }
        public string[] acknowledgeWarnings { get; set; }
    }
}
