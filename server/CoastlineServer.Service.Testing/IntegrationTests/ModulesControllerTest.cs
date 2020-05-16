using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            var getRequest = CreateHttpRequest(HttpMethod.Get, "/modules/");

            // act
            var response = await _client.SendAsync(getRequest);
            var modules = await GetRequestData<IEnumerable<ModuleDto>>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(modules, m => m.Id == -1);
        }
    }
}