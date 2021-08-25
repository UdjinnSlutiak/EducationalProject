// <copyright file="EquipmentLogicTest.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
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
    public class EquipmentLogicTest
    {
        /// <summary>
        /// Tests if EquipmentLogic Get method returns Equipment collection correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfEquipment()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);
            var logic = new EquipmentLogic(controller);

            // Act
            var result = await logic.GetEquipmentsAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new () { Id = 1, Name = "Mob", Price = 300 });
        }

        /// <summary>
        /// Tests if EquipmentLogic Get method returns Equipment instance by Id correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdReturnsEquipment()
        {
            // Arrange
            int testEquipmentId = 2;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);
            var logic = new EquipmentLogic(controller);

            // Act
            var result = await logic.GetEquipmentByIdAsync(testEquipmentId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            result.Equals(this.GetTestEquipments().FirstOrDefault(user => user.Id == testEquipmentId));
        }

        /// <summary>
        /// Tests if EquipmentLogic Create method adds Equipment instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateEquipmentAddsRecord()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Equipment>(context);
            var logic = new EquipmentLogic(controller);

            // Act
            await logic.CreateEquipmentAsync(this.GetTestEquipments().First());
            await context.SaveChangesAsync();

            // Assert
            context.Equipments.Contains(this.GetTestEquipments().First());
        }

        /// <summary>
        /// Tests if EquipmentLogic Update method changes Equipment instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateEquipmentChangesInformation()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);
            var logic = new EquipmentLogic(controller);
            Equipment equipment = this.GetTestEquipments().First();

            // Act
            equipment.Price = 2800;
            await logic.UpdateEquipmentAsync(equipment);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Equipments.First().Price == 2800);
        }

        /// <summary>
        /// Tests if EquipmentLogic Delete method removes Equipment instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteEquipmentRemovesEquipment()
        {
            // Arrange
            var testUserId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);
            var logic = new EquipmentLogic(controller);
            var equipment = this.GetTestEquipments().First();

            // Act
            await logic.DeleteEquipmentAsync(testUserId);
            await context.SaveChangesAsync();

            // Assert
            Assert.False(context.Equipments.Contains(equipment));
        }

        /// <summary>
        /// Creates Equipments collection.
        /// </summary>
        /// <returns>Equipments List.</returns>
        private List<Equipment> GetTestEquipments()
        {
            return new List<Equipment>
            {
                new () { Id = 1, Name = "Mob", Price = 300 },
                new () { Id = 2, Name = "Pan", Price = 500 },
                new () { Id = 3, Name = "Bucket", Price = 80 },
            };
        }
    }
}
