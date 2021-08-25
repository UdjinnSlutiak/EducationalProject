// <copyright file="UserRepositoryTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.UnitTests.Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class UserRepositoryTest
    {
        /// <summary>
        /// Tests if UserRepository Get method returns collection of Users correctly
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfUsers()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);

            // Act
            var result = await controller.GetAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new () { Id = 3, FirstName = "Zosia", LastName = "Petrova", Role = "Cleaner", PasswordHash = "hashed_34567", Username = "zosia_the_cleaner" });
        }

        /// <summary>
        /// Test if UserRepository Get method returns User instance by Id correctly
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdReturnsUser()
        {
            // Arrange
            var testUserId = 3;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestUsers());
            var controller = new Repository<User>(context);

            // Act
            var result = await controller.GetAsync(testUserId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestUsers().FirstOrDefault(user => user.Id == testUserId));
        }

        /// <summary>
        /// Test if UserRepository Create method adds User instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateUserAddsUser()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<User>(context);

            // Act
            await controller.CreateAsync(this.GetTestUsers().First());
            await context.SaveChangesAsync();

            // Assert
            context.Users.Contains(this.GetTestUsers().First());
        }

        /// <summary>
        /// Test if UserRepository Update method changes User instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateUserChangesInfrmation()
        {
            // Arrange
            var testUserId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<User>(context);
            var user = this.GetTestUsers().First(u => u.Id == testUserId);
            user.Id = testUserId;

            // Act
            user.Role = "Policeman";
            await controller.UpdateAsync(user);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Users.Where(u => u.Id == testUserId).First().Role == "Policeman");
        }

        /// <summary>
        /// Test if UserRepository Delete method removes user correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteUserRemovesUser()
        {
            // Arrange
            var testUserId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<User>(context);
            context.AddRange(this.GetTestUsers());
            var user = this.GetTestUsers().First(u => u.Id == testUserId);

            // Act
            await controller.DeleteAsync(testUserId);

            // Assert
            Assert.False(context.Users.Contains(user));
        }

        /// <summary>
        /// Creates Users collection.
        /// </summary>
        /// <returns>IQueryable Users List as IQueryable.</returns>
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
