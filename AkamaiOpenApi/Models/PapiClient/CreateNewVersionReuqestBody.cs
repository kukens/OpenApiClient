using Newtonsoft.Json;

namespace AkamaiOpenApi.Models.PapiClient
{
    public class CreateNewVersionReuqestBody
    {
        public int createFromVersion { get; set; }
        public string createFromVersionEtag { get; set; }
    }
}
