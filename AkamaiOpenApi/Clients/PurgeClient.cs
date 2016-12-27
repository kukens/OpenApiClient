using AkamaiOpenApi.Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace AkamaiOpenApi.Clients
{
    public class PurgeClient : AkamaiOpenApiClient
    {
        public PurgeClient(ClientCredentials credentials, string groupId, string contractId) : base(credentials)
        {
            this.groupId = groupId;
            this.contractId = contractId;
        }

        private string groupId;
        private string contractId;

    }
}

