using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Tests.Domain
{
    public class UserRepositoryTest
    {

        [Fact]
        public void GetReturnsListOfUsers()
        {

            //Arrange
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Users).Returns(GetTestUsers());
            var controller = new UserRepository(mock.Object);

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
            var testUserId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Users).Returns(GetTestUsers());
            var controller = new UserRepository(mock.Object);

            //Act
            var result = controller.Get(testUserId);

            //Assert
            Assert.NotNull(result);
            result.Equals(GetTestUsers().FirstOrDefault(user => user.Id == testUserId));

        }

        [Fact]
        public void CreateUserAddsUser()
        {
            //Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new UserRepository(mock.Object);

            //Act
            controller.Create(GetTestUsers().First());

            //Assert
            mock.Verify(r => r.Add(GetTestUsers().First()));
        }

        [Fact]
        public void UpdateUserChangesInfrmation()
        {
            //Arrange
            var testUserId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new UserRepository(mock.Object);
            var user = GetTestUsers().First(u => u.Id == testUserId);

            //Act
            controller.Update(testUserId, user);

            //Assert
            mock.Verify(r => r.Update(user));

        }

        [Fact]
        public void DeleteUserRemovesUser()
        {
            //Arrange
            var testUserId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Users).Returns(GetTestUsers());
            var controller = new UserRepository(mock.Object);
            var user = GetTestUsers().First(u => u.Id == testUserId);

            //Act
            controller.Delete(testUserId);

            //Assert
            mock.Verify(r => r.Remove<User>(user));
        }

        private IQueryable<User> GetTestUsers()
        {
            return new List<User> {
                new() { Id = 1, Name = "Petro", Position = "Cleaner" },
                new() { Id = 2, Name = "Ostap", Position = "Cook" },
                new() { Id = 3, Name = "Zosia", Position = "Director" }

            }.AsQueryable();
        }
        
    }
}
