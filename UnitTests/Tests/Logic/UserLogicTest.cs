using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Domain.Models;
using Domain.Repositories;

namespace UnitTests.Tests.Logic
{
    public class UserLogicTest
    {

        [Fact]
        public void GetReturnsUsersList()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Get()).Returns(GetTestUsers());
            var controller = new UserLogic(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new() { Id = 3, Name = "Zosia", Position = "Director" });
        }

        [Fact]
        public void GetByIdReturnsUser()
        {
            //Arrange
            int testUserId = 2;
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Get(testUserId))
                .Returns(GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
            var controller = new UserLogic(mock.Object);

            //Act
            var result = controller.Get(testUserId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            result.Equals(GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
        }

        [Fact]
        public void CreateUserAddsUser()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);

            //Act
            controller.Create(GetTestUsers().First());

            //Assert
            mock.Verify(r => r.Create(GetTestUsers().First()));
        }

        [Fact]
        public void UpdateUserChangesInformation()
        {
            //Arrange
            var testUserId = 1;
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);
            var user = GetTestUsers().First(u => u.Id == testUserId);

            //Act
            controller.Update(testUserId, user);

            //Assert
            mock.Verify(r => r.Update(testUserId, user));
        }

        [Fact]
        public void DeleteUserRemovesUser()
        {

            //Arrange
            var testUserId = 1;
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);

            //Act
            controller.Delete(testUserId);

            //Assert
            mock.Verify(r => r.Delete(testUserId));
        }

        private static List<User> GetTestUsers()
        {
            return new()
            {
                new() { Id = 1, Name = "Petro", Position = "Cleaner" },
                new() { Id = 2, Name = "Ostap", Position = "Cook" },
                new() { Id = 3, Name = "Zosia", Position = "Director" }
            };
        }
    }
}
