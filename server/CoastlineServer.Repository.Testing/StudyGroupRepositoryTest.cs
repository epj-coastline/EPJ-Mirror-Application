using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class StudyGroupRepositoryTest : IDisposable
    {
        private SqliteConnection Connection { get; set; }
        private readonly StudyGroupRepository _studyGroupRepository;
        private StudyGroup StudyGroup { get; set; }

        public StudyGroupRepositoryTest()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            var options = new DbContextOptionsBuilder<CoastlineContext>()
                .UseSqlite(Connection)
                .Options;
            var context = new CoastlineContext(options);
            context.Database.EnsureCreatedAsync();
            _studyGroupRepository = new StudyGroupRepository(context);
        }

        public void Dispose()
        {
            Connection.Close();
        }

        [Fact]
        public async Task GetAll_ReturnsAllStudyGroups()
        {
            // arrange & act
            var studyGroups = await _studyGroupRepository.GetAll();
            
            // assert
            Assert.Equal(5, studyGroups.Count);
            Assert.Contains(studyGroups, s => s.Id == -1);
        }

        [Fact]
        public async Task GetAll_ReturnsStudyGroupsWithMembers()
        {
            // arrange
            var studyGroups = await _studyGroupRepository.GetAll();
            
            // act
            var membersOfStudyGroup = studyGroups.Find(s => s.Id == -1).Members;
            
            // assert
            Assert.NotEmpty(membersOfStudyGroup);
        }
        
        [Fact]
        public async Task GetAll_ReturnsStudyGroupsWithCreators()
        {
            // arrange
            var studyGroups = await _studyGroupRepository.GetAll();
            
            // act
            var creator = studyGroups.Find(s => s.Id == -1).User;
            
            // assert
            Assert.NotNull(creator);
            Assert.Equal(-1, creator.Id);
        }
        
        [Fact]
        public async Task Get_SingleStudyGroupById_ReturnsStudyGroup()
        {
            // arrange & act
            StudyGroup = await _studyGroupRepository.Get(-1);

            // assert
            Assert.Equal(-1, StudyGroup.Id);
            Assert.NotNull(StudyGroup.User);
        }
        
        [Fact]
        public async Task Get_SingleStudyGroupByInvalidId_ThrowsException()
        {
            // arrange
            var invalidStudyGroupId = -500;

            // act & assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _studyGroupRepository.Get(invalidStudyGroupId));
        }
    }
}