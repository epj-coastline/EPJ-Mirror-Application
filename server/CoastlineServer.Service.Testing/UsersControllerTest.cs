using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing
{
    public class UsersControllerTest
    {
        private readonly HttpClient _client;

        public UsersControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsersTest()
        {
            // arrange & act
            var response = await _client.GetAsync("/users/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userDtos = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(userDtos, u => u.Id == -1);
        }


        [Fact]
        public async Task GetUserTest()
        {
            // arrange
            var userId = -1;

            // act
            var response = await _client.GetAsync($"/users/{userId}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userId, userDto.Id);
        }

        [Fact]
        public async Task PostAndDeleteUserTest()
        {
            // arrange
            var userDto = new UserDto
            {
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS2020"
            };
            var content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");
            
            // act
            var postResponse = await _client.PostAsync("/users/", content);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<UserDto>(stringResponse);

            // arrange
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal(userDto.FirstName, responseDto.FirstName);

            // act
            var query = postResponse.Headers.Location.PathAndQuery;
            
            // act
            var deleteResponse = await _client.DeleteAsync(query);
            deleteResponse.EnsureSuccessStatusCode();

            // assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}