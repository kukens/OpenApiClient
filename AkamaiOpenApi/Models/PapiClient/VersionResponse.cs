using Newtonsoft.Json;

namespace AkamaiOpenApi.Models.PapiClient
{
    public class VersionResponse
    {
        public string propertyId { get; set; }
        public string propertyName { get; set; }
        public string accountId { get; set; }
        public string contractId { get; set; }
        public string groupId { get; set; }
        public string ruleFormat { get; set; }
        public Versions versions { get; set; }
    }

    public class Versions
    {
        public VersionItem[] items { get; set; }
    }

    public class VersionItem
    {
        public int propertyVersion { get; set; }
        public string updatedByUser { get; set; }
        public string updatedDate { get; set; }
        public string productionStatus { get; set; }
        public string stagingStatus { get; set; }
        public string etag { get; set; }
        public string productId { get; set; }
        public string note { get; set; }
    }

    public class VersionLink
    {
        public string versionLink { get; set; }
    }

    }
