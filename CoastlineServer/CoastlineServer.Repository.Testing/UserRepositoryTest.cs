using System;
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
        public void InsertTest()
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
            User = UserRepository.Insert(newUser);

            // assert
            Assert.Equal(User.FirstName, newUser.FirstName);
        }

        [Fact]
        public void GetTest()
        {
            // act
            User = UserRepository.Get(-1);

            // assert
            Assert.Equal("David", User.FirstName);
        }

        [Fact]
        public void GetAllTest()
        {
            // act
            var users = UserRepository.GetAll();

            // assert
            Assert.Equal(4, users.Count);
            Assert.Contains(users, u => u.Id == -1);
        }

        [Fact]
        public void UpdateTest()
        {
            // arrange
            User = UserRepository.Get(-1);
            User.Biography = "This is a test";

            // act
            UserRepository.Update(User);
            var updatedUser = UserRepository.Get(-1);

            // assert
            Assert.Equal(User.Biography, updatedUser.Biography);
        }

        [Fact]
        public void DeleteTest()
        {
            // arrange
            User = UserRepository.Get(-2);

            // act
            UserRepository.Delete(User);

            // assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                UserRepository.Get(User.Id));
        }
    }
}