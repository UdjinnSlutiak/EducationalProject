using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace UnitTests.Tests.Domain
{
    public class UserRepositoryTest
    {
        
        [Fact]
        public void GetReturnsListOfUsers()
        {

            //Arrange
            var mock = new Mock<IUser>();
            mock.Setup(repo => repo.Get()).Returns(GetTestUsers());
            //var controller = new UserRepository(mock.Object);

            //Act


            //Assert

        }

        private static List<User> GetTestUsers()
        {
            return new()
            {
                new() { Id = 1, Name = "Petro", Position = "Cleaner" },
                new() { Id = 2, Name = "Ostap", Position = "Cook"},
                new() { Id = 3, Name = "Zosia", Position = "Director"}
            };
        }
        
    }
}
