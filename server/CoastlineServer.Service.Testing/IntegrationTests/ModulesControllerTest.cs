using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using CoastlineServer.Service.Testing.TestHelper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class ModulesControllerTest
    {
        private readonly HttpClient _client;
        private readonly string _accessToken;
        private readonly AuthenticationHeaderValue _authenticationHeader;

        public ModulesControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _accessToken = Auth0Helper.GetAccessToken();
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        [Fact]
        public async Task GetAll_ReturnsAllModules()
        {
            // arrange
            var getRequest = new HttpRequestMessage(HttpMethod.Get, "/modules/");
            getRequest.Headers.Authorization = _authenticationHeader;
            
            // //act
            var response = await _client.SendAsync(getRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var moduleDtos = JsonConvert.DeserializeObject<IEnumerable<ModuleDto>>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(moduleDtos, m => m.Id == -1);
        }
    }
}