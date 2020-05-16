using RestSharp;

namespace CoastlineServer.Service.Testing.TestHelper
{
    public static class Auth0Helper
    {
        public static string GetAccessToken()
        {
            var client = new RestClient("https://dev-coastline.eu.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                "{\"client_id\":\"vpbqiuRoAklW81E8CGu4BV5G7lGclVJs\"," +
                "\"client_secret\":\"zJvPrNwLPkdbxvn69xz6dcSXAkDMOXY_kvpJPawOWw2a7b4bHbVZAvEdZYvzksHP\"," +
                "\"audience\":\"http://localhost:5000\",\"grant_type\":\"client_credentials\"}",
                ParameterType.RequestBody);
            
            IRestResponse response = client.Execute(request);
            var content = response.Content.Replace("\"", "").Split(':');
            var accessToken = content[1].Split(',')[0];

            return accessToken;
        }
    }
}