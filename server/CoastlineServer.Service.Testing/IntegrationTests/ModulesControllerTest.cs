// using System.Collections.Generic;
// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
// using CoastlineServer.Service.Models;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Newtonsoft.Json;
// using Xunit;
//
// namespace CoastlineServer.Service.Testing.IntegrationTests
// {
//     public class ModulesControllerTest
//     {
//         private readonly HttpClient _client;
//
//         public ModulesControllerTest()
//         {
//             var appFactory = new WebApplicationFactory<Startup>();
//             _client = appFactory.CreateClient();
//         }
//
//         [Fact]
//         public async Task GetAll_ReturnsAllModules()
//         {
//             // arrange & act
//             var response = await _client.GetAsync("/modules/");
//             response.EnsureSuccessStatusCode();
//             var stringResponse = await response.Content.ReadAsStringAsync();
//             var moduleDtos = JsonConvert.DeserializeObject<IEnumerable<ModuleDto>>(stringResponse);
//             
//             // assert
//             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//             Assert.Contains(moduleDtos, m => m.Id == -1);
//         }
//     }
// }