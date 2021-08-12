using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Tests.Domain
{
    public class EquipmentRepositoryTest
    {

        [Fact]
        public void GetReturnsListOfEquipments()
        {

            //Arrange
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Equipments).Returns(GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(new() { Id = 3, Name = "Bucket", Value = 80 });

        }

        [Fact]
        public void GetByIdReturnsEquipment()
        {
            //Arrange
            var testEquipmentId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Equipments).Returns(GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);

            //Act
            var result = controller.Get(testEquipmentId);

            //Assert
            Assert.NotNull(result);
            result.Equals(GetTestEquipments().FirstOrDefault(Equipment => Equipment.Id == testEquipmentId));

        }

        [Fact]
        public void CreateEquipmentAddsEquipment()
        {
            //Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new EquipmentRepository(mock.Object);

            //Act
            controller.Create(GetTestEquipments().First());

            //Assert
            mock.Verify(r => r.Add(GetTestEquipments().First()));
        }

        [Fact]
        public void UpdateEquipmentChangesInfrmation()
        {
            //Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new EquipmentRepository(mock.Object);
            var Equipment = GetTestEquipments().First(u => u.Id == testEquipmentId);

            //Act
            controller.Update(testEquipmentId, Equipment);

            //Assert
            mock.Verify(r => r.Update(Equipment));

        }

        [Fact]
        public void DeleteEquipmentRemovesEquipment()
        {
            //Arrange
            var testEquipmentId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Equipments).Returns(GetTestEquipments());
            var controller = new EquipmentRepository(mock.Object);
            var Equipment = GetTestEquipments().First(u => u.Id == testEquipmentId);

            //Act
            controller.Delete(testEquipmentId);

            //Assert
            mock.Verify(r => r.Remove<Equipment>(Equipment));
        }

        private IQueryable<Equipment> GetTestEquipments()
        {
            return new List<Equipment> {
                new() { Id = 1, Name = "Mob", Value = 300 },
                new() { Id = 2, Name = "Pan", Value = 500 },
                new() { Id = 3, Name = "Bucket", Value = 80}

            }.AsQueryable();
        }

    }
}
