using System;
using System.Linq;
using CoastlineServer.DAL.Entities;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _target;
        public User User { get; set; }

        public UserRepositoryTest(UserRepository repository)
        {
            _target = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Fact]
        public void GetUserTest()
        {
            // arrange & act
            User = _target.Get(-1);
            // assert
            Assert.Equal(-1, User.Id);
            Assert.Equal("David", User.FirstName);
        }

        [Fact]
        public void InsertAndDeleteUserTest()
        {
            // arrange
            User = new User {FirstName = "Markus", LastName = "Müller"};
            // act
            var newUser = _target.Insert(User);
            // assert
            Assert.Equal(User.FirstName, newUser.FirstName);
            _target.Delete(User);
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _target.Get(User.Id)
            );
        }

        [Fact]
        public void GetAllUsers()
        {
            // arrange & act
            var users = _target.GetAll();
            // assert
            Assert.Equal("David", users.ElementAt(0).FirstName);
            Assert.Equal("Fabian", users.ElementAt(1).FirstName);
        }
    }
}