// <copyright file="EquipmentRepositoryTest.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.UnitTests.Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class EquipmentRepositoryTest
    {
        /// <summary>
        /// Tests if EquipmentRepository Get method returns Equipments collection correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfEquipments()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);

            // Act
            var result = await controller.GetAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new () { Id = 3, Name = "Bucket", Price = 80 });
        }

        /// <summary>
        /// Tests if EquipmentRepository Get method returns Equipment instance by Id correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdReturnsEquipment()
        {
            // Arrange
            var testEquipmentId = 3;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);

            // Act
            var result = await controller.GetAsync(testEquipmentId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestEquipments().FirstOrDefault(equipment => equipment.Id == testEquipmentId));
        }

        /// <summary>
        /// Tests if EquipmentRepository Create method adds Equipment instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateEquipmentAddsEquipment()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Equipment>(context);
            var equipment = this.GetTestEquipments().First();

            // Act
            await controller.CreateAsync(equipment);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Equipments.Contains(this.GetTestEquipments().First()));
        }

        /// <summary>
        /// Tests if EquipmentRepository Update method chahges Equipment instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateEquipmentChangesInfrmation()
        {
            // Arrange
            var testEquipmentId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Equipment>(context);
            var equipment = this.GetTestEquipments().First(u => u.Id == testEquipmentId);

            // Act
            equipment.Price = 1200;
            await controller.UpdateAsync(equipment);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Equipments.Where(e => e.Id == testEquipmentId).First().Price == 1200);
        }

        /// <summary>
        /// Tests if EquipmentRepository Delete method removes Equipment instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteEquipmentRemovesEquipment()
        {
            // Arrange
            var testEquipmentId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestEquipments());
            var controller = new Repository<Equipment>(context);
            var equipment = this.GetTestEquipments().First(u => u.Id == testEquipmentId);

            // Act
            await controller.DeleteAsync(testEquipmentId);
            await context.SaveChangesAsync();

            // Assert
            Assert.False(context.Equipments.Contains(equipment));
        }

        /// <summary>
        /// Creates Equipments collection.
        /// </summary>
        /// <returns>IQueryable Equipments List as IQueryable.</returns>
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
