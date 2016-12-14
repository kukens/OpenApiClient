using AkamaiOpenApi.Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace AkamaiOpenApi.Clients
{
    public class PapiClient : AkamaiOpenApiClient
    {
        public PapiClient(ClientCredentials credentials, string groupId, string contractId) : base(credentials)
        {
            this.groupId = groupId;
            this.contractId = contractId;
        }

        private string groupId;
        private string contractId;

        public string GetProperty(string propertyId)
        {
            WebClient webClient = new WebClient();
            string enpointPath = string.Format("/papi/v0/properties/{0}?contractId={1}&groupId={2}", propertyId, this.contractId, this.groupId);
            return this.SendRequest(string.Format("/papi/v0/properties/{0}?contractId={1}&groupId={2}", propertyId, this.contractId, this.groupId));
        }

        private int GetPropertyLatestVersion(string propertyId)
        {
            PropertyItem propertyItem = JsonConvert.DeserializeObject<Properties>(this.GetProperty(propertyId)).properties.items.FirstOrDefault();
            return propertyItem.latestVersion;
        }

        public string GetPropertyRulesTree(string propertyId, int propertyVersion)
        {
            WebClient webClient = new WebClient();
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/{1}/rules/?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            return this.SendRequest(enpointPath);
        }

        public string UpdatePropertyRulesTree(string propertyId, int propertyVersion, string data)
        {
            NameValueCollection headers = new NameValueCollection() {
                { "Content-Type", "application/vnd.akamai.papirules.latest+json" }
            };
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/{1}/rules/?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            return this.SendRequest(enpointPath, data, "PUT", headers);
        }

        public string UpdatePropertyLatestVersionRulesTree(string propertyId, string data)
        {
            int latestVersion = this.GetPropertyLatestVersion(propertyId);
            return this.UpdatePropertyRulesTree(propertyId, latestVersion, data);
        }

        public string CreateNewPropertyVersion(string propertyId, string data)
        {
            NameValueCollection headers = new NameValueCollection() {
                { "Content-Type", "application/json" }
            };
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/?contractId={1}&groupId={2}", propertyId, this.contractId, this.groupId);
            return this.SendRequest(enpointPath, data, "POST", headers);
        }
    }

}

