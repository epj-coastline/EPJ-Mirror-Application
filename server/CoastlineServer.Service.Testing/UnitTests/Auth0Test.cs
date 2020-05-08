using System.Linq;
using RestSharp;
using Xunit;

namespace CoastlineServer.Service.Testing.UnitTests
{
    public class Auth0Test
    {
        [Fact]
        public void Post_Auth0ClientInfo_ReturnsBearerToken()
        {
            // arrange
            var client = new RestClient("https://dev-coastline.eu.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                "{\"client_id\":\"vpbqiuRoAklW81E8CGu4BV5G7lGclVJs\"," +
                "\"client_secret\":\"zJvPrNwLPkdbxvn69xz6dcSXAkDMOXY_kvpJPawOWw2a7b4bHbVZAvEdZYvzksHP\"," +
                "\"audience\":\"http://localhost:5000\",\"grant_type\":\"client_credentials\"}",
                ParameterType.RequestBody);

            // act
            IRestResponse response = client.Execute(request);
            var content = response.Content.Replace("\"", "").Split(':');
            var accessToken = content[1];
            var tokenType = content.Last();

            // assert
            Assert.True(response.IsSuccessful);
            Assert.NotNull(accessToken);
            Assert.Contains("Bearer", tokenType);
        }
    }
}