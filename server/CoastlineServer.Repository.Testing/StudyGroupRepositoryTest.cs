using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository.Parameters;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class StudyGroupRepositoryTest : RepositoryBaseTest
    {
        private readonly StudyGroupRepository _studyGroupRepository;
        private StudyGroup StudyGroup { get; set; }

        public StudyGroupRepositoryTest()
        {
            _studyGroupRepository = new StudyGroupRepository(Context);
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
            var membersOfStudyGroup = studyGroups.Single(s => s.Id == -1).Members;

            // assert
            Assert.NotEmpty(membersOfStudyGroup);
        }

        [Fact]
        public async Task GetAll_ReturnsStudyGroupsWithCreators()
        {
            // arrange
            var studyGroups = await _studyGroupRepository.GetAll();

            // act
            var creator = studyGroups.Single(s => s.Id == -1).User;

            // assert
            Assert.NotNull(creator);
            Assert.Equal("1fo9wW1Ul6I", creator.Id);
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

        [Fact]
        public async Task Insert_SingleStudyGroup_ReturnStudyGroup()
        {
            // arrange
            var newStudyGroup = new StudyGroup()
            {
                CreationDate = new DateTime(2020, 04, 16),
                Purpose = "test studygroup",
                UserId = "1fo9wW1Ul6I",
                ModuleId = -2
            };

            // act
            StudyGroup = await _studyGroupRepository.Insert(newStudyGroup);

            // assert
            Assert.Equal(newStudyGroup.Purpose, StudyGroup.Purpose);
        }

        [Fact]
        public async Task Update_SingleStudyGroup()
        {
            // arrange
            StudyGroup = await _studyGroupRepository.Get(-1);
            StudyGroup.Purpose = "This is a test";

            // act
            await _studyGroupRepository.Update(StudyGroup);
            var updatedStudyGroup = await _studyGroupRepository.Get(-1);

            // assert
            Assert.Equal(StudyGroup.Purpose, updatedStudyGroup.Purpose);
        }

        [Fact]
        public async Task Delete_SingleStudyGroup_ThrowsException()
        {
            // arrange
            StudyGroup = await _studyGroupRepository.Get(-1);

            // act
            await _studyGroupRepository.Delete(StudyGroup);

            // assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _studyGroupRepository.Get(StudyGroup.Id));
        }

        [Fact]
        public async Task Delete_SingleInvalidStudyGroup_ThrowsException()
        {
            // arrange
            StudyGroup = new StudyGroup()
            {
                Id = 500,
                Purpose = "Invalid",
                UserId = "1"
            };

            // act & assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _studyGroupRepository.Delete(StudyGroup));
        }

        [Fact]
        public async Task GetAll_ResourceParameters_ReturnsAllStudyGroupsOfParameter()
        {
            // arrange
            var studyGroupResourceParameters = new StudyGroupResourceParameters()
            {
                Module = "-1"
            };

            // act
            var studyGroups = await _studyGroupRepository.GetAll(studyGroupResourceParameters);

            // assert
            Assert.NotEmpty(studyGroups);
            foreach (var studyGroup in studyGroups)
            {
                Assert.Equal(-1, studyGroup.ModuleId);
            }
        }

        [Fact]
        public async Task GetAll_InvalidResourceParameters_ThrowsException()
        {
            // arrange
            var studyGroupResourceParameters = new StudyGroupResourceParameters()
            {
                Module = "abc"
            };

            // act & assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _studyGroupRepository.GetAll(studyGroupResourceParameters);
            });
        }

        [Fact]
        public async Task GetAll_ResourceParametersNull_ThrowsException()
        {
            // arrange
            StudyGroupResourceParameters studyGroupResourceParameters = null;

            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _studyGroupRepository.GetAll(studyGroupResourceParameters);
            });
        }

        [Fact]
        public async Task GetAll_OutOfRangeResourceParameters_ReturnsEmptyCollection()
        {
            // arrange
            var studyGroupResourceParameters = new StudyGroupResourceParameters()
            {
                Module = "-500"
            };

            // act
            var studyGroups = await _studyGroupRepository.GetAll(studyGroupResourceParameters);

            // assert
            Assert.Empty(studyGroups);
        }
    }
}