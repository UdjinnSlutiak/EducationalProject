// <copyright file="EquipmentRepositoryTest.cs" company="Eugene Slutiak">
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
    public class EquipmentRepositoryTest
    {
        /// <summary>
        /// Tests if EquipmentRepository Get method returns Equipments collection correctly.
        /// </summary>
        [Fact]
        public void GetReturnsListOfEquipments()
        {
            // Arrange
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Equipments).Returns(this.GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new() { Id = 3, Name = "Bucket", Value = 80 });
        }

        /// <summary>
        /// Tests if EquipmentRepository Get method returns Equipment instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsEquipment()
        {
            // Arrange
            var testEquipmentId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Equipments).Returns(this.GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);

            // Act
            var result = controller.Get(testEquipmentId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestEquipments().FirstOrDefault(Equipment => Equipment.Id == testEquipmentId));
        }

        /// <summary>
        /// Tests if EquipmentRepository Create method adds Equipment instance correctly.
        /// </summary>
        [Fact]
        public void CreateEquipmentAddsEquipment()
        {
            // Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new EquipmentRepository(mock.Object);
            var equipment = this.GetTestEquipments().First();

            // Act
            controller.Create(equipment);

            // Assert
            mock.Verify(r => r.Add<Equipment>(equipment));
        }

        /// <summary>
        /// Tests if EquipmentRepository Update method chahges Equipment instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateEquipmentChangesInfrmation()
        {
            // Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new EquipmentRepository(mock.Object);
            var equipment = this.GetTestEquipments().First(u => u.Id == testEquipmentId);

            // Act
            controller.Update(testEquipmentId, equipment);

            // Assert
            mock.Verify(r => r.Update(equipment));
        }

        /// <summary>
        /// Tests if EquipmentRepository Delete method removes Equipment instance correctly.
        /// </summary>
        [Fact]
        public void DeleteEquipmentRemovesEquipment()
        {
            // Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Equipments).Returns(this.GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);
            var equipment = this.GetTestEquipments().First(u => u.Id == testEquipmentId);

            // Act
            controller.Delete(testEquipmentId);

            // Assert
            mock.Verify(r => r.Remove<Equipment>(equipment));
        }

        /// <summary>
        /// Creates Equipments collection.
        /// </summary>
        /// <returns>IQueryable Equipments List as IQueryable.</returns>
        private IQueryable<Equipment> GetTestEquipments()
        {
            return new List<Equipment>
            {
                new() { Id = 1, Name = "Mob", Value = 300 },
                new() { Id = 2, Name = "Pan", Value = 500 },
                new() { Id = 3, Name = "Bucket", Value = 80 }
            }.AsQueryable();
        }
    }
}
