// <copyright file="UserLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.UnitTests.Tests.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfUsers()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);
            var logic = new UserLogic(controller);

            // Act
            var result = await logic.GetUsersAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new () { Id = 3, FirstName = "Zosia", LastName = "Petrova", Role = "Cleaner", PasswordHash = "hashed_34567", Username = "zosia_the_cleaner" });
        }

        /// <summary>
        /// Tests if UserLogic Get method returns User instance by Id correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdReturnsUser()
        {
            // Arrange
            int testUserId = 2;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);
            var logic = new UserLogic(controller);

            // Act
            var result = await logic.GetUserByIdAsync(testUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            result.Equals(this.GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
        }

        /// <summary>
        /// Tests if UserLogic Create method adds User instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateUserAddsUser()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<User>(context);
            var logic = new UserLogic(controller);

            // Act
            await logic.CreateUserAsync(this.GetTestUsers().First());
            await context.SaveChangesAsync();

            // Assert
            context.Users.Contains(this.GetTestUsers().First());
        }

        /// <summary>
        /// Tests if UserLogic Update method changes User instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateUserChangesInformation()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);
            var logic = new UserLogic(controller);
            User user = this.GetTestUsers().First();

            // Act
            user.Role = "Policeman";
            await logic.UpdateUserAsync(user);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Users.First().Role == "Policeman");
        }

        /// <summary>
        /// Tests if UserLogic Delete method removes User instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteUserRemovesUser()
        {
            // Arrange
            var testUserId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);
            var logic = new UserLogic(controller);
            var user = this.GetTestUsers().First();

            // Act
            await logic.DeleteUserAsync(testUserId);

            // Assert
            Assert.False(context.Users.Contains(user));
        }

        /// <summary>
        /// Creates Users collection.
        /// </summary>
        /// <returns>Records List.</returns>
        private List<User> GetTestUsers()
        {
            return new List<User>
            {
                new () { Id = 1, FirstName = "Petro", LastName = "Mostavchuk", Role = "Motivator", PasswordHash = "hashed_12345", Username = "mc_petia" },
                new () { Id = 2, FirstName = "Ostap", LastName = "Korobenko", Role = "Cook", PasswordHash = "hashed_23456", Username = "korobka" },
                new () { Id = 3, FirstName = "Zosia", LastName = "Petrova", Role = "Cleaner", PasswordHash = "hashed_34567", Username = "zosia_the_cleaner" },
            };
        }
    }
}
