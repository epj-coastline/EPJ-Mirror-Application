using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using Xunit;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class StudyGroupsControllerTest : ControllerBaseTest
    {
        [Fact]
        public async Task GetAll_ReturnsAllStudyGroups()
        {
            // arrange
            var getRequest = CreateHttpRequest(HttpMethod.Get, "/studygroups/");

            // //act
            var response = await _client.SendAsync(getRequest);
            var studyGroups = await GetRequestData<IEnumerable<StudyGroupDto>>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(studyGroups, s => s.Id == -1);
        }

        [Fact]
        public async Task Get_SingleStudyGroupById_ReturnsStudyGroupDto()
        {
            // arrange
            var studyGroupId = -1;
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/studygroups/{studyGroupId}");

            // act
            var response = await _client.SendAsync(getRequest);
            var studyGroupDto = await GetRequestData<StudyGroupDto>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(studyGroupId, studyGroupDto.Id);
        }

        [Fact]
        public async Task Get_SingleStudyGroupByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidStudyGroupId = -500;
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/studygroups/{invalidStudyGroupId}");

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

            var postRequestUser = CreateHttpRequest(HttpMethod.Post, "/users/", userForCreationDto);
            var postResponseUser = await _client.SendAsync(postRequestUser);

            try
            {
                var studyGroupForCreationDto = new StudyGroupForCreationDto()
                {
                    Purpose = "Test study group",
                    ModuleId = -1
                };

                var postRequestStudyGroup =
                    CreateHttpRequest(HttpMethod.Post, "/studygroups/", studyGroupForCreationDto);

                // act
                var postResponseStudyGroup = await _client.SendAsync(postRequestStudyGroup);

                var fetchedStudyGroup = await GetRequestData<StudyGroupDto>(postResponseStudyGroup);

                // assert
                Assert.Equal(HttpStatusCode.Created, postResponseStudyGroup.StatusCode);
                Assert.Equal(studyGroupForCreationDto.Purpose, fetchedStudyGroup.Purpose);
                Assert.NotNull(fetchedStudyGroup.Module);
                Assert.NotNull(fetchedStudyGroup.User);

                // arrange
                var queryForStudyGroup = postResponseStudyGroup.Headers.Location.PathAndQuery;
                var deleteRequestStudyGroup = CreateHttpRequest(HttpMethod.Delete, queryForStudyGroup);

                // act
                var deleteResponseStudyGroup = await _client.SendAsync(deleteRequestStudyGroup);

                // assert
                Assert.Equal(HttpStatusCode.OK, deleteResponseStudyGroup.StatusCode);
            }
            finally
            {
                // clean up
                var queryForUser = postResponseUser.Headers.Location.PathAndQuery;
                var deleteRequestUser = CreateHttpRequest(HttpMethod.Delete, queryForUser);

                var deleteResponseUser = await _client.SendAsync(deleteRequestUser);
                deleteResponseUser.EnsureSuccessStatusCode();
            }
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
            var postRequest = CreateHttpRequest(HttpMethod.Post, "/studygroups/", studyGroupForCreationDto);

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
            var deleteRequest = CreateHttpRequest(HttpMethod.Delete, $"/studygroups/{invalidStudyGroupId}");

            // act
            var response = await _client.SendAsync(deleteRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_Parameter_ReturnsStudyGroupsOfModule()
        {
            // arrange
            var moduleId = -1;
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/studygroups?module={moduleId}");

            // act
            var response = await _client.SendAsync(getRequest);
            var fetchedStudyGroups = await GetRequestData<IEnumerable<StudyGroupDto>>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(fetchedStudyGroups, s => s.ModuleId == -1);
        }

        [Fact]
        public async Task GetAll_InvalidParameter_ReturnsNotFound()
        {
            // arrange
            var invalidModuleId = "abc";
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/studygroups?module={invalidModuleId}");

            // act
            var response = await _client.SendAsync(getRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}