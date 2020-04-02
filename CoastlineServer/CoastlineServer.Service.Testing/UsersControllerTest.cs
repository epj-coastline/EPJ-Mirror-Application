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

        [Theory]
        [InlineData("POST")]
        public async Task PostUserTest(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/users/");

            var newUser = new UserDto
            {
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS2020"
            };
            var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            // act
            var response = await _client.PostAsync("/users/", content);
            response.EnsureSuccessStatusCode();

            // assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}