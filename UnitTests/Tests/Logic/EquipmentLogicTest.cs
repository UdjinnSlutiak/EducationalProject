// <copyright file="EquipmentLogicTest.cs" company="Eugene Slutiak">
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
    public class EquipmentLogicTest
    {
        /// <summary>
        /// Tests if EquipmentLogic Get method returns Equipment collection correctly.
        /// </summary>
        [Fact]
        public void GetReturnsEquipmentList()
        {
            // Arrange
            var mock = new Mock<IEquipmentRepository>();
            mock.Setup(repo => repo.Get()).Returns(this.GetTestEquipments());
            var controller = new EquipmentLogic(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _ = Assert.IsAssignableFrom<IEnumerable<Equipment>>(result);
            result.Contains(new() { Id = 3, Name = "Bucket", Value = 80 });
            Assert.Equal(3, result.Count());
        }

        /// <summary>
        /// Tests if EquipmentLogic Get method returns Equipment instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsEquipment()
        {
            // Arrange
            int testEquipmentId = 3;
            var mock = new Mock<IEquipmentRepository>();
            mock.Setup(repo => repo.Get(testEquipmentId))
                .Returns(this.GetTestEquipments().FirstOrDefault(user => user.Id == testEquipmentId));
            var controller = new EquipmentLogic(mock.Object);

            // Act
            var result = controller.Get(testEquipmentId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Equipment>(result);
            result.Equals(this.GetTestEquipments().FirstOrDefault(user => user.Id == testEquipmentId));
        }

        /// <summary>
        /// Tests if EquipmentLogic Create method adds Equipment instance correctly.
        /// </summary>
        [Fact]
        public void CreateEquipmentAddsEquipment()
        {
            // Arrange
            var mock = new Mock<IEquipmentRepository>();
            var controller = new EquipmentLogic(mock.Object);
            var equipment = this.GetTestEquipments().First();

            // Act
            controller.Create(equipment);

            // Assert
            mock.Verify(r => r.Create(equipment));
        }

        /// <summary>
        /// Tests if EquipmentLogic Update method changes Equipment instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateEquipmentChangesInformation()
        {
            // Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IEquipmentRepository>();
            var controller = new EquipmentLogic(mock.Object);
            var equipment = this.GetTestEquipments().First(e => e.Id == testEquipmentId);

            // Act
            controller.Update(testEquipmentId, equipment);

            // Assert
            mock.Verify(r => r.Update(testEquipmentId, equipment));
        }

        /// <summary>
        /// Tests if EquipmentLogic Delete method removes Equipment instance correctly.
        /// </summary>
        [Fact]
        public void DeleteEquipmentRemovesEquipment()
        {
            // Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IEquipmentRepository>();
            var controller = new EquipmentLogic(mock.Object);

            // Act
            controller.Delete(testEquipmentId);

            // Assert
            mock.Verify(r => r.Delete(testEquipmentId));
        }

        /// <summary>
        /// Creates Equipments collection.
        /// </summary>
        /// <returns>Equipments List.</returns>
        private List<Equipment> GetTestEquipments()
        {
            return new()
            {
                new() { Id = 1, Name = "Mob", Value = 300 },
                new() { Id = 2, Name = "Pan", Value = 500 },
                new() { Id = 3, Name = "Bucket", Value = 80 }
            };
        }
    }
}
