// <copyright file="UserRepositoryTest.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.UnitTests.Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class UserRepositoryTest
    {
        /// <summary>
        /// Tests if UserRepository Get method returns collection of Users correctly
        /// </summary>
        [Fact]
        public void GetReturnsListOfUsers()
        {
            // Arrange
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Users).Returns(this.GetTestUsers());
            var controller = new UserRepository(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new() { Id = 3, Name = "Zosia", Position = "Director" });
        }

        /// <summary>
        /// Test if UserRepository Get method returns User instance by Id correctly
        /// </summary>
        [Fact]
        public void GetByIdReturnsUser()
        {
            // Arrange
            var testUserId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Users).Returns(this.GetTestUsers());
            var controller = new UserRepository(mock.Object);

            // Act
            var result = controller.Get(testUserId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
        }

        /// <summary>
        /// Test if UserRepository Create method adds User instance correctly.
        /// </summary>
        [Fact]
        public void CreateUserAddsUser()
        {
            // Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new UserRepository(mock.Object);

            // Act
            controller.Create(this.GetTestUsers().First());

            // Assert
            mock.Verify(r => r.Add(this.GetTestUsers().First()));
        }

        /// <summary>
        /// Test if UserRepository Update method changes User instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateUserChangesInfrmation()
        {
            // Arrange
            var testUserId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new UserRepository(mock.Object);
            var user = this.GetTestUsers().First(u => u.Id == testUserId);

            // Act
            controller.Update(testUserId, user);

            // Assert
            mock.Verify(r => r.Update(user));
        }

        /// <summary>
        /// Test if UserRepository Delete method removes user correctly.
        /// </summary>
        [Fact]
        public void DeleteUserRemovesUser()
        {
            // Arrange
            var testUserId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Users).Returns(this.GetTestUsers());
            var controller = new UserRepository(mock.Object);
            var user = this.GetTestUsers().First(u => u.Id == testUserId);

            // Act
            controller.Delete(testUserId);

            // Assert
            mock.Verify(r => r.Remove<User>(user));
        }

        /// <summary>
        /// Creates Users collection.
        /// </summary>
        /// <returns>IQueryable Users List as IQueryable.</returns>
        private IQueryable<User> GetTestUsers()
        {
            return new List<User>
            {
                new() { Id = 1, Name = "Petro", Position = "Cleaner" },
                new() { Id = 2, Name = "Ostap", Position = "Cook" },
                new() { Id = 3, Name = "Zosia", Position = "Director" }
            }.AsQueryable();
        }
    }
}
