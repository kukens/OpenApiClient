using Newtonsoft.Json;
using System.Collections.Specialized;

namespace AkamaiOpenApi.Models.Constants
{
    public class Enviroment
    {
        public static string staging = "STAGING";
        public static string production = "PPRODUCTION";
    }

    public class RequestHeaders
    {
        public static NameValueCollection ApplicationJsonContentType = new NameValueCollection() {
                { "Content-Type", "application/json" }
            };

        public static NameValueCollection PapiRulesContentType(string papiRulesVersion)
        {
            if (string.IsNullOrEmpty(papiRulesVersion))
            {
                return new NameValueCollection() { { "Content-Type", "application/vnd.akamai.papirules.latest+json" } };
            }

            return new NameValueCollection() { { "Content-Type", string.Format("application/vnd.akamai.papirules.v{0}+json", papiRulesVersion) };
        }
    }
}

