using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AkamaiOpenApi.Clients
{
    public abstract class AkamaiOpenApiClient
    {
        public AkamaiOpenApiClient(ClientCredentials credentials)
        {
            this.host = credentials.host;
            this.clientToken = credentials.clientToken;
            this.accessToken = credentials.accessToken;
            this.clientSecret = credentials.clientSecret;
        }

        protected string host;
        protected string clientToken;
        protected string clientSecret;
        protected string accessToken;

        private UTF8Encoding encoding = new UTF8Encoding();

        private string getBase64Hash(string secret, string input)
        {
            HMACSHA256 hmacClientSecret = new HMACSHA256(this.encoding.GetBytes(secret));
            byte[] inputHash = hmacClientSecret.ComputeHash(this.encoding.GetBytes(input));
            return Convert.ToBase64String(inputHash);
        }

        private string getBase64Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            SHA256 sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(this.encoding.GetBytes(input)));
        }


        protected string SendRequest(string endpointPath, string requestBody = "", string method = "GET", NameValueCollection headers = null)
        {
            WebClient webClient = new WebClient();

            if (headers != null)
            {
                webClient.Headers.Add(headers);
            }

            string timestamp = DateTime.UtcNow.ToString("yyyyMMddTHH:mm:ss+0000");

            StringBuilder authorizationHeader = new StringBuilder();

            authorizationHeader.AppendFormat("EG1-HMAC-SHA256 client_token={0};", this.clientToken);
            authorizationHeader.AppendFormat("access_token={0};", this.accessToken);
            authorizationHeader.AppendFormat("timestamp={0};", timestamp);
            authorizationHeader.AppendFormat("nonce={0};", Guid.NewGuid().ToString());

            string requestBodyBase64Hash = method == "POST"? getBase64Hash(requestBody) : string.Empty;

            string signingKey = getBase64Hash(this.clientSecret, timestamp);
            string dataToSign = string.Format("{0}\thttps\t{1}\t{2}\t\t{3}\t{4}", method, this.host, endpointPath, requestBodyBase64Hash, authorizationHeader.ToString());

            webClient.Headers.Add("Authorization", string.Format("{0}signature={1}", authorizationHeader.ToString(), getBase64Hash(signingKey, dataToSign)));

            Console.WriteLine("Sending request: " + endpointPath);

            ServicePointManager.Expect100Continue = false;

            try
            {
                if (method == "PUT" || method == "POST")
                {
                    return webClient.UploadString(string.Format("https://{0}{1}", host, endpointPath), method, requestBody);
                }
                else
                {
                    return webClient.DownloadString(string.Format("https://{0}{1}", host, endpointPath));
                }
            }
            catch (WebException ex)
            {
                return new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
        }
    }
}
