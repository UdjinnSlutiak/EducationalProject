// <copyright file="RecordRepositoryTest.cs" company="Eugene Slutiak">
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
    using Record = EquipmentControll.Domain.Models.Record;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class RecordRepositoryTest
    {
        /// <summary>
        /// Tests if RecordRepository Get method returns Records collection correctly.
        /// </summary>
        [Fact]
        public void GetReturnsListOfRecords()
        {
            // Arrange
            List<Record> records = this.GetTestRecords().ToList();
            List<string> recordStrings = new();
            foreach (var item in records)
            {
                recordStrings.Add($"{item.Sender} gave {item.Receiver} {item.Equipment}");
            }

            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Records).Returns(this.GetTestRecords());
            var controller = new RecordRepository(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains("Petro gave Ostap Mob");
        }

        /// <summary>
        /// Tests if RecordRepository Get method returns Record instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsRecord()
        {
            // Arrange
            var testRecordId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Records).Returns(this.GetTestRecords());
            var controller = new RecordRepository(mock.Object);

            // Act
            var result = controller.Get(testRecordId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestRecords().FirstOrDefault(Record => Record.Id == testRecordId));
        }

        /// <summary>
        /// Tests if RecordRepository Create method adds Record instance correctly.
        /// </summary>
        [Fact]
        public void CreateRecordAddsRecord()
        {
            // Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new RecordRepository(mock.Object);
            var record = this.GetTestRecords().First();
            record.SenderId = record.Sender.Id;
            record.ReceiverId = record.Receiver.Id;
            record.EquipmentId = record.Equipment.Id;

            // Act
            controller.Create(record);

            // Assert
            mock.Verify(r => r.Add<Record>(record));
        }

        /// <summary>
        /// Tests if RecordRepository Update method changes Record instance information correctly.
        /// </summary>
        [Fact]
        public void UpdateRecordChangesInfrmation()
        {
            // Arrange
            var testRecordId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new RecordRepository(mock.Object);
            var record = this.GetTestRecords().First(u => u.Id == testRecordId);

            // Act
            controller.Update(testRecordId, record);

            // Assert
            mock.Verify(r => r.Update<Record>(record));
        }

        /// <summary>
        /// Tests if RecordRepository Delete method removes Record instance correctly.
        /// </summary>
        [Fact]
        public void DeleteRecordRemovesRecord()
        {
            // Arrange
            var testRecordId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Records).Returns(this.GetTestRecords());
            var controller = new RecordRepository(mock.Object);
            var record = this.GetTestRecords().First(u => u.Id == testRecordId);

            // Act
            controller.Delete(testRecordId);

            // Assert
            mock.Verify(r => r.Remove<Record>(record));
        }

        /// <summary>
        /// Creates Records collection.
        /// </summary>
        /// <returns>IQueryable Records List as IQueryable.</returns>
        private IQueryable<Record> GetTestRecords()
        {
            Equipment mob = new() { Id = 1, Name = "Mob", Value = 300 };
            Equipment pan = new() { Id = 2, Name = "Pan", Value = 500 };

            User petro = new() { Id = 1, Name = "Petro", Position = "Cleaner" };
            User ostap = new() { Id = 2, Name = "Ostap", Position = "Cook" };

            return new List<Record>
            {
                new() { Id = 1, Sender = petro, Receiver = ostap, Equipment = mob },
                new() { Id = 2, Sender = petro, Receiver = ostap, Equipment = pan },
                new() { Id = 3, Sender = ostap, Receiver = petro, Equipment = mob }
            }.AsQueryable();
        }
    }
}
