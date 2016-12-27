using AkamaiOpenApi.Models.PapiClient;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using AkamaiOpenApi.Models.Constants;

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

        /// <summary>
        /// Lists all properties
        /// </summary>
        /// <returns>List of properties</returns>
        public PropertyItem[] ListProperties()
        {
            string enpointPath = string.Format("/papi/v0/properties/?contractId={1}&groupId={2}", this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<Properties>(response).items;
        }

        /// <summary>
        /// Gets a property
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <returns>A property</returns>
        public PropertyItem GetProperty(string propertyId)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}?contractId={1}&groupId={2}", propertyId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<PropertiesResponse>(response).properties.items.FirstOrDefault();
        }

        /// <summary>
        /// Gets property version
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="propertyVersion">Property version</param>
        /// <returns>Version object</returns>
        public VersionItem[] ListVersion(string propertyId)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/?contractId={2}&groupId={3}", propertyId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<VersionResponse>(response).versions.items;
        }

        /// <summary>
        /// Gets property version
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="propertyVersion">Property version</param>
        /// <returns>Version object</returns>
        public VersionItem GetVersion(string propertyId, int propertyVersion)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/{1}/?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<VersionResponse>(response).versions.items.FirstOrDefault();
        }

        /// <summary>
        /// Gets property version
        /// </summary>
        /// <param name="versionLink">Version endpoint link</param>
        /// <returns>Version object</returns>
        public VersionItem GetVersion(string versionLink)
        {
            string response = this.SendRequest(versionLink);
            return JsonConvert.DeserializeObject<VersionResponse>(response).versions.items.FirstOrDefault();
        }

        /// <summary>
        /// Gets property latest version
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="propertyVersion">Property version</param>
        /// <returns>Latest version endpoint link</returns>
        public string GetLatestVersion(string propertyId, int propertyVersion)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/latest?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<VersionLink>(response).versionLink;
        }

        /// <summary>
        /// Creates new property version
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="requestBody"></param>
        /// <returns>Created version endpoint link</returns>
        public string CreateNewVersion(string propertyId, CreateNewVersionReuqestBody requestBody)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/?contractId={1}&groupId={2}", propertyId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath, JsonConvert.SerializeObject(requestBody), "POST", RequestHeaders.ApplicationJsonContentType);
            return JsonConvert.DeserializeObject<VersionLink>(response).versionLink;
        }

        /// <summary>
        /// Gets a rule tree
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="propertyVersion">Property version</param>
        /// <returns>Rule tree JSON string</returns>
        public string GetRuleTree(string propertyId, int propertyVersion, string papiRulesVersion = null)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/{1}/rules/?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            return string.IsNullOrEmpty(papiRulesVersion) ? this.SendRequest(enpointPath) : this.SendRequest(enpointPath, null, "GET", RequestHeaders.PapiRulesContentType(papiRulesVersion));
        }

        /// <summary>
        /// Updates a rule tree
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="propertyVersion">Property version</param>
        /// <param name="data"></param>
        /// <returns>Updated rule tree JSON string</returns>
        public string UpdateRuleTree(string propertyId, int propertyVersion, string data, string papiRulesVersion = null)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/versions/{1}/rules/?contractId={2}&groupId={3}", propertyId, propertyVersion, this.contractId, this.groupId);
            return this.SendRequest(enpointPath, data, "PUT", RequestHeaders.PapiRulesContentType(papiRulesVersion));
        }

        /// <summary>
        /// Updates a rule tree
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="requestData"></param>
        /// <returns>Activation link</returns>
        public string  ActivateProperty(string propertyId, ActivatePropertyReuqestBody requestData)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/activations/?contractId={2}&groupId={3}", propertyId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath, JsonConvert.SerializeObject(requestData), "POST", RequestHeaders.ApplicationJsonContentType);
            return JsonConvert.DeserializeObject<ActivationLink>(response).acivationLink;
        }

        /// <summary>
        /// Gets an activation
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="activationId">Activation ID</param>
        /// <returns>Activation item</returns>
        public ActivationItem GetActivation(string propertyId, string activationId)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/activations/{1}/?contractId={2}&groupId={3}", propertyId, activationId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<ActivationResponse>(response).activations.items.FirstOrDefault();
        }

        /// <summary>
        /// Gets an activation
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <param name="activationId">Activation ID</param>
        /// <returns>Activation JSON string</returns>
        public ActivationItem[] ListActivations(string propertyId)
        {
            string enpointPath = string.Format("/papi/v0/properties/{0}/activations/?contractId={2}&groupId={3}", propertyId, this.contractId, this.groupId);
            string response = this.SendRequest(enpointPath);
            return JsonConvert.DeserializeObject<ActivationResponse>(response).activations.items;
        }
    }
}

