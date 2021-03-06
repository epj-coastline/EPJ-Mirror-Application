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
    public class UserRepositoryTest : RepositoryBaseTest
    {
        private readonly UserRepository _userRepository;
        private User User { get; set; }

        public UserRepositoryTest()
        {
            _userRepository = new UserRepository(Context);
        }

        [Fact]
        public async Task Insert_SingleUser_ReturnsCreatedUser()
        {
            // arrange
            var newUser = new User
            {
                Id = "-1",
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "This is a test",
                DegreeProgram = "Testing",
                StartDate = "HS20"
            };

            // act
            User = await _userRepository.Insert(newUser);

            // assert
            Assert.Equal(newUser.FirstName, User.FirstName);
        }

        [Fact]
        public async Task Get_SingleUserById_ReturnsUser()
        {
            // arrange & act
            User = await _userRepository.Get("1fo9wW1Ul6I");

            // assert
            Assert.Equal("Mathias", User.FirstName);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUser()
        {
            // arrange & act
            var users = await _userRepository.GetAll();

            // assert
            Assert.Equal(4, users.Count);
            Assert.Contains(users, u => u.Id == "1fo9wW1Ul6I");
        }

        [Fact]
        public async Task Update_SingleUser()
        {
            // arrange
            User = await _userRepository.Get("1fo9wW1Ul6I");
            User.Biography = "This is a test";

            // act
            await _userRepository.Update(User);
            var updatedUser = await _userRepository.Get("1fo9wW1Ul6I");

            // assert
            Assert.Equal(User.Biography, updatedUser.Biography);
        }

        [Fact]
        public async Task Delete_SingleUser_ThrowsException()
        {
            // arrange
            User = await _userRepository.Get("2GqPPUoB4R7");

            // act
            await _userRepository.Delete(User);

            // assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userRepository.Get(User.Id));
        }

        [Fact]
        public async Task Get_SingleUserByInvalidId_ThrowsException()
        {
            // arrange
            var invalidUserId = "-500";

            // act & assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userRepository.Get(invalidUserId));
        }

        [Fact]
        public async Task Delete_SingleInvalidUser_ThrowsException()
        {
            // arrange
            User = new User
            {
                Id = "-500",
                FirstName = "Invalid",
                LastName = "Invalid"
            };

            // act & assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _userRepository.Delete(User));
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsersWithStudyGroups()
        {
            // arrange
            var users = await _userRepository.GetAll();

            // act
            var studyGroups = users.Single(u => u.Id == "1fo9wW1Ul6I").StudyGroups;

            // assert
            Assert.NotEmpty(studyGroups);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsersWithStrengths()
        {
            // arrange
            var users = await _userRepository.GetAll();

            // act
            var strengths = users.Single(u => u.Id == "1fo9wW1Ul6I").Strengths;

            // assert
            Assert.NotEmpty(strengths);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsersWithMembers()
        {
            // arrange
            var users = await _userRepository.GetAll();

            // act
            var members = users.Single(u => u.Id == "1fo9wW1Ul6I").Members;

            // assert
            Assert.NotEmpty(members);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsersWithConfirmations()
        {
            // arrange
            var users = await _userRepository.GetAll();

            // act
            var confirmations = users.Single(u => u.Id == "3bPWlzE5nx1").Confirmations;

            // assert
            Assert.NotEmpty(confirmations);
        }

        [Fact]
        public async Task GetALL_ResourceParameters_ReturnsUsersForStrength()
        {
            // arrange
            var userResourceParameters = new UserResourceParameters()
            {
                Strength = "-1"
            };

            // act
            var users = await _userRepository.GetAll(userResourceParameters);
            var strengthsOfUser = users.First().Strengths;
            var strengthOfModul = strengthsOfUser.Single(s => s.ModuleId == -1);

            // assert
            Assert.NotEmpty(users);
            Assert.NotNull(strengthOfModul);
        }

        [Fact]
        public async Task GetAll_InvalidResourceParameters_ThrowsException()
        {
            // arrange
            var userResourceParameters = new UserResourceParameters()
            {
                Strength = "abc"
            };

            // act
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _userRepository.GetAll(userResourceParameters);
            });
        }

        [Fact]
        public async Task GetAll_ResourceParametersNull_ThrowsException()
        {
            // arrange
            UserResourceParameters userResourceParameters = null;

            // act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _userRepository.GetAll(userResourceParameters);
            });
        }

        [Fact]
        public async Task GetAll_OutOfRangeResourceParameters_ReturnsEmptyCollection()
        {
            // arrange
            var userResourceParameters = new UserResourceParameters()
            {
                Strength = "-500"
            };

            // act
            var users = await _userRepository.GetAll(userResourceParameters);

            // assert
            Assert.Empty(users);
        }
    }
}