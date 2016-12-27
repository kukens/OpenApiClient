using Newtonsoft.Json;

namespace AkamaiOpenApi.Models.PapiClient
{
    public class ActivationResponse
    {
        public Activations activations { get; set; }
    }

    public class Activations
    {
        public ActivationItem[] items { get; set; }
    }

    public class ActivationItem
    {
        public string activationId { get; set; }
        public string propertyName { get; set; }
        public string propertyId { get; set; }
        public int propertyVersion { get; set; }
        public string network { get; set; }
        public string activationType { get; set; }
        public string status { get; set; }
        public string submitDate { get; set; }
        public string updateDate { get; set; }
        public string note { get; set; }
        public string[] notifyEmails { get; set; }
        public string accountId { get; set; }
        public string groupId { get; set; }
    }

    public class ActivationLink
    {
        public string acivationLink { get; set; }
    }

}

