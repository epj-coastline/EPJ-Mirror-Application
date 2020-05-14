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
    public class StudyGroupsControllerTest
    {
        private readonly HttpClient _client;
        private readonly string _accessToken;
        private readonly AuthenticationHeaderValue _authenticationHeader;

        public StudyGroupsControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _accessToken = Auth0Helper.GetAccessToken();
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", _accessToken);
            
        }

        [Fact]
        public async Task GetAll_ReturnsAllStudyGroups()
        {
            // arrange
            var getRequest = new HttpRequestMessage(HttpMethod.Get, "/studygroups/");
            getRequest.Headers.Authorization = _authenticationHeader;
            
            // //act
            var response = await _client.SendAsync(getRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var studyGroupDtos = JsonConvert.DeserializeObject<IEnumerable<StudyGroupDto>>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(studyGroupDtos, s => s.Id == -1);
        }

        [Fact]
        public async Task Get_SingleStudyGroupById_ReturnsStudyGroupDto()
        {
            // arrange
            var studyGroupId = -1;
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/studygroups/{studyGroupId}");
            getRequest.Headers.Authorization = _authenticationHeader;

            // act
            var response = await _client.SendAsync(getRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var studyGroupDto = JsonConvert.DeserializeObject<StudyGroupDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(studyGroupId, studyGroupDto.Id);
        }

        [Fact]
        public async Task Get_SingleStudyGroupByInvalidId_ReturnsNotFound()
        {
            // arrange
            var studyGroupId = -500;
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/studygroups/{studyGroupId}");
            getRequest.Headers.Authorization = _authenticationHeader;

            // act
            var response = await _client.SendAsync(getRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostDelete_SingleStudyGroup_ReturnsNoContent()
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
            var jStringContent = new StringContent(JsonConvert.SerializeObject(userForCreationDto), Encoding.UTF8,
                "application/json");
            var postUserRequest = new HttpRequestMessage(HttpMethod.Post, "/users/")
            {
                Content = jStringContent
            };
            postUserRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var studyGroupForCreationDto = new StudyGroupForCreationDto()
            {
                Purpose = "Test study group",
                ModuleId = -1
            };
            var content = new StringContent(JsonConvert.SerializeObject(studyGroupForCreationDto), Encoding.UTF8,
                "application/json");
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/studygroups/")
            {
                Content = content
            };
            postRequest.Headers.Authorization = _authenticationHeader;

            // act
            var postResponseUser = await _client.SendAsync(postUserRequest);
            postResponseUser.EnsureSuccessStatusCode();

            var postResponse = await _client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<StudyGroupDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal(studyGroupForCreationDto.Purpose, responseDto.Purpose);
            Assert.NotNull(responseDto.Module);
            Assert.NotNull(responseDto.User);

            // arrange
            var query = postResponse.Headers.Location.PathAndQuery;
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, query);
            deleteRequest.Headers.Authorization = _authenticationHeader;

            var queryForUser = postResponseUser.Headers.Location.PathAndQuery;
            var deleteUserRequest = new HttpRequestMessage(HttpMethod.Delete, queryForUser);
            deleteUserRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // act
            var deleteResponse = await _client.SendAsync(deleteRequest);
            deleteResponse.EnsureSuccessStatusCode();

            var deleteUserResponse = await _client.SendAsync(deleteUserRequest);
            deleteUserResponse.EnsureSuccessStatusCode();

            // assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Post_InvalidStudyGroup_ReturnsBadRequest()
        {
            // arrange
            var studyGroupForCreationDto = new StudyGroupForCreationDto()
            {
                Purpose = "",
                ModuleId = -1
            };
            var content = new StringContent(JsonConvert.SerializeObject(studyGroupForCreationDto), Encoding.UTF8,
                "application/json");
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/studygroups/")
            {
                Content = content
            };
            postRequest.Headers.Authorization = _authenticationHeader;

            // act
            var response = await _client.SendAsync(postRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_SingleStudyGroupByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidStudyGroupId = -500;
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"/studygroups/{invalidStudyGroupId}");
            deleteRequest.Headers.Authorization = _authenticationHeader;

            // act
            var response = await _client.SendAsync(deleteRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_Parameter_ReturnsStudyGroupsOfModule()
        {
            // arrange
            var getRequest = new HttpRequestMessage(HttpMethod.Get, "/studygroups?module=-1");
            getRequest.Headers.Authorization = _authenticationHeader;
            
            // act
            var response = await _client.SendAsync(getRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var studyGroupDtos = JsonConvert.DeserializeObject<IEnumerable<StudyGroupDto>>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(studyGroupDtos, s => s.ModuleId == -1);
        }

        [Fact]
        public async Task GetAll_InvalidParameter_ReturnsNotFound()
        {
            // arrange
            var getRequest = new HttpRequestMessage(HttpMethod.Get, "/studygroups?module=abc");
            getRequest.Headers.Authorization = _authenticationHeader;
            
            // act
            var response = await _client.SendAsync(getRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}