using System;
using System.Linq;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class UserRepositoryTest
    {
        public UserRepository UserRepository { get; set; }
        public User User { get; set; }

        [Fact]
        public void InsertTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<CoastlineContext>()
                    .UseSqlite(connection)
                    .Options;
                var newUser = new User
                {
                    FirstName = "Markus",
                    LastName = "Christen",
                    Email = "markus.christen@hsr.ch",
                    Biography = "this is a test",
                    DegreeProgram = "Testing",
                    StartDate = "HS2020"
                };
                using (var context = new CoastlineContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new CoastlineContext(options))
                {
                    UserRepository = new UserRepository(context);
                    User = UserRepository.Insert(newUser);
                    Assert.Equal(User.FirstName, newUser.FirstName);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<CoastlineContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CoastlineContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new CoastlineContext(options))
                {
                    UserRepository = new UserRepository(context);
                    User = UserRepository.Get(-1);
                    Assert.Equal("David", User.FirstName);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetAllTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<CoastlineContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CoastlineContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new CoastlineContext(options))
                {
                    UserRepository = new UserRepository(context);
                    var users = UserRepository.GetAll();
                    Assert.Equal(4, users.Count);
                    Assert.Equal(-4, users.ElementAt(0).Id);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void UpdateTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<CoastlineContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CoastlineContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new CoastlineContext(options))
                {
                    UserRepository = new UserRepository(context);
                    User = UserRepository.Get(-1);
                    User.Biography = "This is a test";
                    UserRepository.Update(User);
                    var updatedUser = UserRepository.Get(-1);
                    Assert.Equal(User.Biography, updatedUser.Biography);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public void DeleteTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<CoastlineContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new CoastlineContext(options))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new CoastlineContext(options))
                {
                    UserRepository = new UserRepository(context);
                    var ex = Assert.Throws<NullReferenceException>(() =>
                        UserRepository.Get(User.Id));
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}