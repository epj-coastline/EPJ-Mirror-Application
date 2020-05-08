using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using CoastlineServer.Service.Testing.TestHelper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class UsersControllerTest
    {
        private readonly HttpClient _client;
        private readonly string _accessToken;

        public UsersControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _accessToken = Auth0Helper.GetAccessToken();
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsers()
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
        public async Task Get_SingleUserById_ReturnsUser()
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
        public async Task PostDelete_SingleUser_ReturnsNoContent()
        {
            // arrange
            var userForCreationDto = new UserForCreationDto()
            {
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS20"
            };
            var content = new StringContent(JsonConvert.SerializeObject(userForCreationDto), Encoding.UTF8,
                "application/json");
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/users/") {Content = content};
            postRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // act
            var postResponse = await _client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<UserDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal(userForCreationDto.FirstName, responseDto.FirstName);

            // arrange
            var query = postResponse.Headers.Location.PathAndQuery;
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, query);
            deleteRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // act
            var deleteResponse = await _client.SendAsync(deleteRequest);
            deleteResponse.EnsureSuccessStatusCode();

            // assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Get_SingleUserByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidUserId = -500;

            // act
            var response = await _client.GetAsync($"/users/{invalidUserId}");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_SingleUserByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidStudyGroupId = -500;
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"/users/{invalidStudyGroupId}");
            deleteRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // act
            var response = await _client.SendAsync(deleteRequest);


            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_SingleInvalidUser_ReturnsBadRequest()
        {
            // arrange
            var userForCreationDto = new UserForCreationDto()
            {
                FirstName = "",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS2020"
            };
            var content = new StringContent(JsonConvert.SerializeObject(userForCreationDto), Encoding.UTF8,
                "application/json");
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/users/") {Content = content};
            postRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // act
            var postResponse = await _client.SendAsync(postRequest);
            
            // assert
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task GetAll_Parameter_ReturnsUsersWithStrengthInModule()
        {
            // arrange & act
            var response = await _client.GetAsync("/users?strength=-1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userDtos = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(userDtos);
        }

        [Fact]
        public async Task GetAllUsers_InvalidParameter_ReturnsNotFound()
        {
            // arrange & act
            var response = await _client.GetAsync("/users?strength=abc");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}