using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CoastlineServer.Service.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace CoastlineServer.Service.Testing
{
    public class UsersControllerTest
    {
        private readonly HttpClient _client;

        public UsersControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder().AddJsonFile("F:\\HSR\\04_Sem\\99_EPJ\\06_Application\\application\\CoastlineServer\\CoastlineServer.Service\\appsettings.Development.json").Build())
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllUsersTest(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/users/");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userDtos = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(stringResponse);
            
            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(userDtos, u => u.Id == -1);
        }


        [Theory]
        [InlineData("GET", -1)]
        public async Task GetUserTest(string method, int? userId = null)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/users/{userId}");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(stringResponse);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userId, userDto.Id);
        }
    }
}
