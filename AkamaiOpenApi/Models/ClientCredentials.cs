namespace AkamaiOpenApi
{
    public class ClientCredentials
    {
        public ClientCredentials(string host, string clientToken, string clientSecret, string accessToken)
        {
            this.host = host;
            this.clientToken = clientToken;
            this.clientSecret = clientSecret;
            this.accessToken = accessToken;
    }

        public string host { get; set; }
        public string clientToken { get; set; }
        public string clientSecret { get; set; }
        public string accessToken { get; set; }
    }
}
