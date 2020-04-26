﻿using System.Collections.Generic;
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
         }*/
    }
}