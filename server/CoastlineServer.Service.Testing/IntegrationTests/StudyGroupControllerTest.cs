using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class StudyGroupControllerTest
    {
        private readonly HttpClient _client;

        public StudyGroupControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsAllStudyGroups()
        {
            // arrange & act
            var response = await _client.GetAsync("/studygroups/");
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

            // act
            var response = await _client.GetAsync($"/studygroups/{studyGroupId}");
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

            // act
            var response = await _client.GetAsync($"/studygroups/{studyGroupId}");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostDelete_SingleStudyGroup_ReturnsNoContent()
        {
            // arrange
            var studyGroupForCreationDto = new StudyGroupForCreationDto()
            {
                Purpose = "Test studygroup",
                UserId = -1,
                ModuleId = -1
            };
            var content = new StringContent(JsonConvert.SerializeObject(studyGroupForCreationDto), Encoding.UTF8,
                "application/json");

            // act
            var postResponse = await _client.PostAsync("/studygroups/", content);
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

            // act
            var deleteResponse = await _client.DeleteAsync(query);
            deleteResponse.EnsureSuccessStatusCode();

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
                UserId = -1,
                ModuleId = -1
            };
            var content = new StringContent(JsonConvert.SerializeObject(studyGroupForCreationDto), Encoding.UTF8,
                "application/json");

            // act
            var response = await _client.PostAsync("/studygroups/", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Delete_SingleStudyGroupByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidStudyGroupId = -500;

            // act
            var response = await _client.DeleteAsync($"/studygroups/{invalidStudyGroupId}");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_Parameter_ReturnsStudyGroupsOfModule()
        {
            // arrange & act
            var response = await _client.GetAsync("/studygroups?module=-1");
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
            // arrange & act
            var response = await _client.GetAsync("/studygroups?module=abc");

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        // Adding tests
        public async Task GetAll_ReturnsAllModules()
        {
            // arrange & act
            var response = await _client.GetAsync("/modules/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var moduleDtos = JsonConvert.DeserializeObject<IEnumerable<ModuleDto>>(stringResponse);
            
            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(moduleDtos, m => m.Id == -1);
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

            // act
            var postResponse = await _client.PostAsync("/users/", content);
            postResponse.EnsureSuccessStatusCode();
            var stringResponse = await postResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<UserDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal(userForCreationDto.FirstName, responseDto.FirstName);

            // arrange
            var query = postResponse.Headers.Location.PathAndQuery;

            // act
            var deleteResponse = await _client.DeleteAsync(query);
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
            var invalidUserId = -500;

            // act
            var response = await _client.DeleteAsync($"/users/{invalidUserId}");

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

            // act
            var response = await _client.PostAsync("/users/", content);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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