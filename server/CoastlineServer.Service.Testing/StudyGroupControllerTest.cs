using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing
{
    public class StudyGroupControllerTest
    {
        private readonly HttpClient _client;

        public StudyGroupControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }
        
         /*[Fact]
         public async Task GetAll_ReturnsAllUsers()
         {
             // arrange & act
             var response = await _client.GetAsync("/studygroups/");
             response.EnsureSuccessStatusCode();
             var stringResponse = await response.Content.ReadAsStringAsync();
             var studyGroupDtos = JsonConvert.DeserializeObject<IEnumerable<StudyGroupDto>>(stringResponse);
        
             // assert
             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
             Assert.Contains(studyGroupDtos, u => u.Id == -1);
         }*/
    }
}