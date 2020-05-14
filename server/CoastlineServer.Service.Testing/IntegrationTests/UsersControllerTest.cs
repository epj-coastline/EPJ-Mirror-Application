using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using CoastlineServer.Service.Models;


namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class UsersControllerTest : ControllerBaseTest
    {
        [Fact]
        public async Task GetAll_ReturnsAllUsers()
        {
            // arrange
            var getRequest = CreateHttpRequest(HttpMethod.Get, "/users/");
            int numberOfUsersInSeedData = 4;

            // act
            var response = await _client.SendAsync(getRequest);
            var users = await GetRequestData<ICollection<UserDto>>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(users.Count, numberOfUsersInSeedData);
        }

        [Fact]
        public async Task Get_SingleUserById_ReturnsUser()
        {
            // arrange
            var userId = "1fo9wW1Ul6I";
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/users/{userId}");

            // act
            var response = await _client.SendAsync(getRequest);
            var user = await GetRequestData<UserDto>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userId, user.Id);
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
            var postRequest = CreateHttpRequest(HttpMethod.Post, "/users/", userForCreationDto);

            // act
            var postResponse = await _client.SendAsync(postRequest);
            var userDto = await GetRequestData<UserDto>(postResponse);

            // assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal(userForCreationDto.FirstName, userDto.FirstName);

            // arrange
            var query = postResponse.Headers.Location.PathAndQuery;
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, query);
            deleteRequest.Headers.Authorization = _authenticationHeader;

            // act
            var deleteResponse = await _client.SendAsync(deleteRequest);
            deleteResponse.EnsureSuccessStatusCode();

            // assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Update_SingleUser_ReturnsNoContent()
        {
            // arrange
            var insertedUser = await InsertUser();
            try
            {
                var userForUpdate = new UserDto()
                {
                    Id = insertedUser.Id,
                    FirstName = "Marcus",
                    LastName = "Christen",
                    Email = "markus.christen@hsr.ch",
                    Biography = "this is a test",
                    DegreeProgram = "Testing",
                    StartDate = "HS20"
                };
                var requestUri = $"/users/{userForUpdate.Id}";

                var putRequest = CreateHttpRequest(HttpMethod.Put, requestUri, userForUpdate);
                var getRequest = CreateHttpRequest(HttpMethod.Get, requestUri);

                // act
                var putResponse = await _client.SendAsync(putRequest);
                var getResponse = await _client.SendAsync(getRequest);

                var fetchedUser = await GetRequestData<UserDto>(getResponse);

                // assert
                Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
                Assert.Equal(userForUpdate.FirstName, fetchedUser.FirstName);
            }
            finally
            {
                // clean up
                await DeleteUser(insertedUser.Id);
            }
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            // arrange
            var insertedUser = await InsertUser();
            try
            {
                var wrongUserId = "abc";

                var userForUpdate = new UserDto()
                {
                    Id = insertedUser.Id,
                    FirstName = "Marcus",
                    LastName = "Christen",
                    Email = "markus.christen@hsr.ch",
                    Biography = "this is a test",
                    DegreeProgram = "Testing",
                    StartDate = "HS20"
                };

                var putRequest = CreateHttpRequest(HttpMethod.Put, $"/users/{wrongUserId}", userForUpdate);

                // act
                var putResponse = await _client.SendAsync(putRequest);

                // assert
                Assert.Equal(HttpStatusCode.BadRequest, putResponse.StatusCode);
            }
            finally
            {
                // clean up
                await DeleteUser(insertedUser.Id);
            }
        }

        [Fact]
        public async Task Update_WithWrongId_ReturnsForbidden()
        {
            // arrange
            var insertedUser = await InsertUser();
            try
            {
                var wrongUserId = "abc";

                var userForUpdate = new UserDto()
                {
                    Id = wrongUserId,
                    FirstName = "Marcus",
                    LastName = "Christen",
                    Email = "markus.christen@hsr.ch",
                    Biography = "this is a test",
                    DegreeProgram = "Testing",
                    StartDate = "HS20"
                };

                var putRequest = CreateHttpRequest(HttpMethod.Put, $"/users/{wrongUserId}", userForUpdate);

                // act
                var putResponse = await _client.SendAsync(putRequest);

                // assert
                Assert.Equal(HttpStatusCode.Forbidden, putResponse.StatusCode);
            }
            finally
            {
                // clean up
                await DeleteUser(insertedUser.Id);
            }
        }

        [Fact]
        public async Task Update_NonExistingUser_ReturnsConflict()
        {
            // arrange
            var insertedUser = await InsertUser();
            await DeleteUser(insertedUser.Id);

            var userForUpdate = new UserDto()
            {
                Id = insertedUser.Id,
                FirstName = "Marcus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS20"
            };

            var putRequest = CreateHttpRequest(HttpMethod.Put, $"/users/{insertedUser.Id}", userForUpdate);

            // act
            var putResponse = await _client.SendAsync(putRequest);

            // assert
            Assert.Equal(HttpStatusCode.Conflict, putResponse.StatusCode);
        }

        [Fact]
        public async Task Get_SingleUserByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidUserId = -500;
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/users/{invalidUserId}");

            // act
            var response = await _client.SendAsync(getRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_SingleUserByInvalidId_ReturnsNotFound()
        {
            // arrange
            var invalidUserId = -500;
            var deleteRequest = CreateHttpRequest(HttpMethod.Delete, $"/users/{invalidUserId}");

            // act
            var deleteResponse = await _client.SendAsync(deleteRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
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
            var postRequest = CreateHttpRequest(HttpMethod.Post, "/users/", userForCreationDto);

            // act
            var postResponse = await _client.SendAsync(postRequest);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task GetAll_Parameter_ReturnsUsersWithStrengthInModule()
        {
            // arrange
            var moduleId = -1;
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/users?strength={moduleId}");

            // act
            var response = await _client.SendAsync(getRequest);
            var users = await GetRequestData<IEnumerable<UserDto>>(response);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task GetAllUsers_InvalidParameter_ReturnsNotFound()
        {
            // arrange
            var invalidModuleId = "abc";
            var getRequest = CreateHttpRequest(HttpMethod.Get, $"/users?strength={invalidModuleId}");

            // act
            var response = await _client.SendAsync(getRequest);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<UserDto> InsertUser()
        {
            var userForCreationDto = new UserForCreationDto()
            {
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "this is a test",
                DegreeProgram = "Testing",
                StartDate = "HS20"
            };
            var postRequest = CreateHttpRequest(HttpMethod.Post, "/users/", userForCreationDto);
            var postResponse = await _client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();
            return await GetRequestData<UserDto>(postResponse);
        }

        private async Task DeleteUser(string userId)
        {
            var deleteUserRequest = CreateHttpRequest(HttpMethod.Delete, $"/users/{userId}");
            await _client.SendAsync(deleteUserRequest);
        }
    }
}