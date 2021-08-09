using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Domain.Models;
using Domain.Repositories;
using Record = Domain.Models.Record;

namespace UnitTests.Tests.Logic
{
    public class RecordLogicTest
    {

        [Fact]
        public void GetReturnsRecordList()
        {
            //Arrange
            List<Record> records = GetTestRecords();
            List<string> recordStrings = new();
            foreach (var item in records)
                recordStrings.Add($"{item.Sender} gave {item.Receiver} {item.Equipment}");

            var mock = new Mock<IRecord>();
            mock.Setup(repo => repo.Get()).Returns(recordStrings);
            var controller = new RecordLogic(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _ = Assert.IsAssignableFrom<IEnumerable<string>>(result);
            result.Contains("Petro gave Ostap Mob");
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetByIdReturnsRecord()
        {
            //Arrange
            int testRecordId = 3;
            Record record = GetTestRecords().First(r => r.Id == testRecordId);

            var mock = new Mock<IRecord>();
            mock.Setup(repo => repo.Get(testRecordId))
                .Returns($"{record.Sender} gave {record.Receiver} {record.Equipment}");
            var controller = new RecordLogic(mock.Object);

            //Act
            var result = controller.Get(testRecordId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            result.Equals(GetTestRecords().FirstOrDefault(user => user.Id == testRecordId));
        }

        [Fact]
        public void CreateRecordAddsRecord()
        {
            //Arrange
            var mock = new Mock<IRecord>();
            var controller = new RecordLogic(mock.Object);
            var record = GetTestRecords().First();

            //Act
            controller.Create(record);

            //Assert
            mock.Verify(r => r.Create(record));
        }

        [Fact]
        public void UpdateRecordChangesInformation()
        {
            //Arrange
            var testRecordId = 2;
            var mock = new Mock<IRecord>();
            var controller = new RecordLogic(mock.Object);
            var record = GetTestRecords().First(r => r.Id == testRecordId);

            //Act
            controller.Update(2, record);

            //Assert
            mock.Verify(r => r.Update(2, record));
        }

        [Fact]
        public void DeleteRecordRemovesRecord()
        {
            //Arrange
            var testRecordId = 3;
            var mock = new Mock<IRecord>();
            var controller = new RecordLogic(mock.Object);

            //Act
            controller.Delete(testRecordId);

            //Assert
            mock.Verify(r => r.Delete(testRecordId));

        }

        private static List<Record> GetTestRecords()
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
