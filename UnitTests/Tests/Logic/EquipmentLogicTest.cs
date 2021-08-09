using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Domain.Models;
using Domain.Repositories;

namespace UnitTests.Tests.Logic
{
    public class EquipmentLogicTest
    {

        [Fact]
        public void GetReturnsEquipmentList()
        {
            //Arrange
            var mock = new Mock<IEquipment>();
            mock.Setup(repo => repo.Get()).Returns(GetTestEquipments());
            var controller = new EquipmentLogic(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _ = Assert.IsAssignableFrom<IEnumerable<Equipment>>(result);
            result.Contains(new() { Id = 3, Name = "Bucket", Value = 80 });
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetByIdReturnsEquipment()
        {
            //Arrange
            int testEquipmentId = 3;
            var mock = new Mock<IEquipment>();
            mock.Setup(repo => repo.Get(testEquipmentId))
                .Returns(GetTestEquipments().FirstOrDefault(user => user.Id == testEquipmentId));
            var controller = new EquipmentLogic(mock.Object);

            //Act
            var result = controller.Get(testEquipmentId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Equipment>(result);
            result.Equals(GetTestEquipments().FirstOrDefault(user => user.Id == testEquipmentId));
        }

        [Fact]
        public void CreateEquipmentAddsEquipment()
        {
            //Arrange
            var mock = new Mock<IEquipment>();
            var controller = new EquipmentLogic(mock.Object);
            var equipment = GetTestEquipments().First();

            //Act
            controller.Create(equipment);

            //Assert
            mock.Verify(r => r.Create(equipment));
        }

        [Fact]
        public void UpdateEquipmentChangesInformation()
        {
            //Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IEquipment>();
            var controller = new EquipmentLogic(mock.Object);
            var equipment = GetTestEquipments().First(e => e.Id == testEquipmentId);

            //Act
            controller.Update(testEquipmentId, equipment);

            //Assert
            mock.Verify(r => r.Update(testEquipmentId, equipment));
        }

        [Fact]
        public void DeleteEquipmentRemovesEquipment()
        {

            //Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IEquipment>();
            var controller = new EquipmentLogic(mock.Object);

            //Act
            controller.Delete(testEquipmentId);

            //Assert
            mock.Verify(r => r.Delete(testEquipmentId));
        }

        private static List<Equipment> GetTestEquipments()
        {
            return new()
            {
                new() { Id = 1, Name = "Mob", Value = 300 },
                new() { Id = 2, Name = "Pan", Value = 500 },
                new() { Id = 3, Name = "Bucket", Value = 80}
            };
        }

    }
}
