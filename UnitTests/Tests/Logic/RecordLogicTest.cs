// <copyright file="RecordLogicTest.cs" company="Eugene Slutiak">
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
    using Record = EquipmentControll.Domain.Models.Record;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class RecordLogicTest
    {
        /// <summary>
        /// Tests if RecordLogic Get method returns Record collection correctly.
        /// </summary>
        [Fact]
        public void GetReturnsRecordList()
        {
            // Arrange
            List<Record> records = this.GetTestRecords();
            List<string> recordStrings = new();
            foreach (var item in records)
            {
                recordStrings.Add($"{item.Sender} gave {item.Receiver} {item.Equipment}");
            }

            var mock = new Mock<IRecordRepository>();
            mock.Setup(repo => repo.Get()).Returns(recordStrings);
            var controller = new RecordLogic(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _ = Assert.IsAssignableFrom<IEnumerable<string>>(result);
            result.Contains("Petro gave Ostap Mob");
            Assert.Equal(3, result.Count());
        }

        /// <summary>
        /// Tests if RecordLogic Get method returns Record instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsRecord()
        {
            // Arrange
            int testRecordId = 3;
            Record record = this.GetTestRecords().First(r => r.Id == testRecordId);

            var mock = new Mock<IRecordRepository>();
            mock.Setup(repo => repo.Get(testRecordId))
                .Returns($"{record.Sender} gave {record.Receiver} {record.Equipment}");
            var controller = new RecordLogic(mock.Object);

            // Act
            var result = controller.Get(testRecordId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            result.Equals(this.GetTestRecords().FirstOrDefault(user => user.Id == testRecordId));
        }

        /// <summary>
        /// Tests if RecordLogic Create method adds Record instance correctly.
        /// </summary>
        [Fact]
        public void CreateRecordAddsRecord()
        {
            // Arrange
            var mock = new Mock<IRecordRepository>();
            var controller = new RecordLogic(mock.Object);
            var record = this.GetTestRecords().First();

            // Act
            controller.Create(record);

            // Assert
            mock.Verify(r => r.Create(record));
        }

        /// <summary>
        /// Tests if RecordLogic Update method changes Record instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateRecordChangesInformation()
        {
            // Arrange
            var testRecordId = 2;
            var mock = new Mock<IRecordRepository>();
            var controller = new RecordLogic(mock.Object);
            var record = this.GetTestRecords().First(r => r.Id == testRecordId);

            // Act
            controller.Update(2, record);

            // Assert
            mock.Verify(r => r.Update(2, record));
        }

        /// <summary>
        /// Tests if RecordLogic Delete method removes Record instance correctly.
        /// </summary>
        [Fact]
        public void DeleteRecordRemovesRecord()
        {
            // Arrange
            var testRecordId = 3;
            var mock = new Mock<IRecordRepository>();
            var controller = new RecordLogic(mock.Object);

            // Act
            controller.Delete(testRecordId);

            // Assert
            mock.Verify(r => r.Delete(testRecordId));
        }

        /// <summary>
        /// Creates Records collection.
        /// </summary>
        /// <returns>Records List.</returns>
        private List<Record> GetTestRecords()
        {
            Equipment mob = new() { Id = 1, Name = "Mob", Value = 300 };
            Equipment pan = new() { Id = 2, Name = "Pan", Value = 500 };

            User petro = new() { Id = 1, Name = "Petro", Position = "Cleaner" };
            User ostap = new() { Id = 1, Name = "Ostap", Position = "Cook" };

            return new()
            {
                new() { Id = 1, Sender = petro, Receiver = ostap, Equipment = mob },
                new() { Id = 2, Sender = petro, Receiver = ostap, Equipment = pan },
                new() { Id = 3, Sender = ostap, Receiver = petro, Equipment = mob }
            };
        }
    }
}
