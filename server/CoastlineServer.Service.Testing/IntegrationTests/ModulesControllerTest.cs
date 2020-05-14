using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using CoastlineServer.Service.Models;


namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class ModulesControllerTest : ControllerBaseTest
    {
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