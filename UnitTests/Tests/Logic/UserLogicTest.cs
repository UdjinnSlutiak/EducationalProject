// <copyright file="UserLogicTest.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.UnitTests.Tests.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using EquipmentControll.Logic;
    using Moq;
    using Xunit;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class UserLogicTest
    {
        /// <summary>
        /// Tests if UserLogic Get method returns Users collection correctly.
        /// </summary>
        [Fact]
        public void GetReturnsUsersList()
        {
            // Arrange
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Get()).Returns(this.GetTestUsers());
            var controller = new UserLogic(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new() { Id = 3, Name = "Zosia", Position = "Director" });
        }

        /// <summary>
        /// Tests if UserLogic Get method returns User instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsUser()
        {
            // Arrange
            int testUserId = 2;
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.Get(testUserId))
                .Returns(this.GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
            var controller = new UserLogic(mock.Object);

            // Act
            var result = controller.Get(testUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            result.Equals(this.GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
        }

        /// <summary>
        /// Tests if UserLogic Create method adds User instance correctly.
        /// </summary>
        [Fact]
        public void CreateUserAddsUser()
        {
            // Arrange
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);

            // Act
            controller.Create(this.GetTestUsers().First());

            // Assert
            mock.Verify(r => r.Create(this.GetTestUsers().First()));
        }

        /// <summary>
        /// Tests if UserLogic Update method changes User instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateUserChangesInformation()
        {
            // Arrange
            var testUserId = 1;
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);
            var user = this.GetTestUsers().First(u => u.Id == testUserId);

            // Act
            controller.Update(testUserId, user);

            // Assert
            mock.Verify(r => r.Update(testUserId, user));
        }

        /// <summary>
        /// Tests if UserLogic Delete method removes User instance correctly.
        /// </summary>
        [Fact]
        public void DeleteUserRemovesUser()
        {
            // Arrange
            var testUserId = 1;
            var mock = new Mock<IUserRepository>();
            var controller = new UserLogic(mock.Object);

            // Act
            controller.Delete(testUserId);

            // Assert
            mock.Verify(r => r.Delete(testUserId));
        }

        /// <summary>
        /// Creates Users collection.
        /// </summary>
        /// <returns>Records List.</returns>
        private List<User> GetTestUsers()
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
