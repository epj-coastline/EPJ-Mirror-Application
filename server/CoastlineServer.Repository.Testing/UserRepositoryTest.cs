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
    public class UserRepositoryTest : IDisposable
    {
        private SqliteConnection Connection { get; set; }
        private UserRepository UserRepository { get; set; }
        private User User { get; set; }

        public UserRepositoryTest()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            var options = new DbContextOptionsBuilder<CoastlineContext>()
                .UseSqlite(Connection)
                .Options;
            var context = new CoastlineContext(options);
            context.Database.EnsureCreated();
            UserRepository = new UserRepository(context);
        }

        public void Dispose()
        {
            Connection.Close();
        }

        [Fact]
        public async Task Insert_SingleUser_ReturnsCreatedUser()
        {
            // arrange
            var newUser = new User
            {
                FirstName = "Markus",
                LastName = "Christen",
                Email = "markus.christen@hsr.ch",
                Biography = "This is a test",
                DegreeProgram = "Testing",
                StartDate = "HS2020"
            };

            // act
            User = await UserRepository.Insert(newUser);

            // assert
            Assert.Equal(User.FirstName, newUser.FirstName);
        }

        [Fact]
        public async Task Get_SingleUserById_ReturnsUser()
        {
            // act
            User = await UserRepository.Get(-1);

            // assert
            Assert.Equal("David", User.FirstName);
        }

        [Fact]
        public async Task GetAll_ReturnsAllUser()
        {
            // act
            var users = await UserRepository.GetAll();

            // assert
            Assert.Equal(4, users.Count);
            Assert.Contains(users, u => u.Id == -1);
        }

        [Fact]
        public async Task Update_SingleUser_ReturnsUpdatedUser()
        {
            // arrange
            User = await UserRepository.Get(-1);
            User.Biography = "This is a test";

            // act
            await UserRepository.Update(User);
            var updatedUser = await UserRepository.Get(-1);

            // assert
            Assert.Equal(User.Biography, updatedUser.Biography);
        }

        [Fact]
        public async Task Delete_SingleUser_ThrowsException()
        {
            // arrange
            User = await UserRepository.Get(-2);

            // act
            await UserRepository.Delete(User);

            // assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await UserRepository.Get(User.Id));
        }

        [Fact]
        public async Task Get_SingleUserById_ThrowsException()
        {
            // arrange
            var invalidUserId = -500;

            // act & assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await UserRepository.Get(invalidUserId));
        }
    }
}