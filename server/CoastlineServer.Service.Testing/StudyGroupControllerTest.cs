using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository;
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
             var getUserResponse = await _client.GetAsync($"/users/{-1}");
             getUserResponse.EnsureSuccessStatusCode();
             var stringUserDto = await getUserResponse.Content.ReadAsStringAsync();
             var creator = JsonConvert.DeserializeObject<UserDto>(stringUserDto);
             var studyGroupForCreationDto = new StudyGroupForCreationDto()
             {
                 Purpose = "Test studygroup",
                 UserId = creator.Id,
                 User = creator
             };
             var content = new StringContent(JsonConvert.SerializeObject(studyGroupForCreationDto), Encoding.UTF8, "application/json");

             // act
             var postResponse = await _client.PostAsync("/studygroups/", content);
             postResponse.EnsureSuccessStatusCode();
             var stringResponse = await postResponse.Content.ReadAsStringAsync();
             var responseDto = JsonConvert.DeserializeObject<StudyGroupDto>(stringResponse);
             
             // assert
             Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
             Assert.Equal(studyGroupForCreationDto.Purpose, responseDto.Purpose);
             
             // arrange
             var query = postResponse.Headers.Location.PathAndQuery;
             
             // act
             var deleteResponse = await _client.DeleteAsync(query);
             deleteResponse.EnsureSuccessStatusCode();
             
             // assert
             Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
         }*/
    }
}