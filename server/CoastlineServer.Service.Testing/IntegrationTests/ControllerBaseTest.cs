using System.Net.Http;
using System.Net.Http.Headers;
using CoastlineServer.Service.Testing.TestHelper;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class ControllerBaseTest
    {
        protected readonly HttpClient _client;
        protected readonly string _accessToken;
        protected readonly AuthenticationHeaderValue _authenticationHeader;

        public ControllerBaseTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _accessToken = Auth0Helper.GetAccessToken();
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", _accessToken);
        }
    }
}