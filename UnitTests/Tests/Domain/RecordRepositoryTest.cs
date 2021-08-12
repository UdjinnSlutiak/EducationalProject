using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Record = Domain.Models.Record;

namespace UnitTests.Tests.Domain
{
    public class RecordRepositoryTest
    {

        [Fact]
        public void GetReturnsListOfRecords()
        {
            //Arrange
            List<Record> records = GetTestRecords().ToList();
            List<string> recordStrings = new();
            foreach (var item in records)
                recordStrings.Add($"{item.Sender} gave {item.Receiver} {item.Equipment}");

            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Records).Returns(GetTestRecords());
            var controller = new RecordRepository(mock.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains("Petro gave Ostap Mob");

        }

        [Fact]
        public void GetByIdReturnsRecord()
        {
            //Arrange


            var testRecordId = 3;
            var mock = new Mock<IProjectContext>();
            mock.SetupGet(repo => repo.Records).Returns(GetTestRecords());
            var controller = new RecordRepository(mock.Object);

            //Act
            var result = controller.Get(testRecordId);

            //Assert
            Assert.NotNull(result);
            result.Equals(GetTestRecords().FirstOrDefault(Record => Record.Id == testRecordId));

        }

        [Fact]
        public void CreateRecordAddsRecord()
        {
            //Arrange
            var mock = new Mock<IProjectContext>();
            var controller = new RecordRepository(mock.Object);

            //Act
            controller.Create(GetTestRecords().First());

            //Assert
            mock.Verify(r => r.Add(GetTestRecords().First()));
        }

        [Fact]
        public void UpdateRecordChangesInfrmation()
        {
            //Arrange
            var testRecordId = 1;
            var mock = new Mock<IProjectContext>();
            var controller = new RecordRepository(mock.Object);
            var record = GetTestRecords().First(u => u.Id == testRecordId);

            //Act
            controller.Update(testRecordId, record);

            //Assert
            mock.Verify(r => r.Update<Record>(record));

        }

        [Fact]
        public void DeleteRecordRemovesRecord()
        {
            //Arrange
            var testRecordId = 1;
            var mock = new Mock<IProjectContext>();
            mock.Setup(repo => repo.Records).Returns(GetTestRecords());
            var controller = new RecordRepository(mock.Object);
            var record = GetTestRecords().First(u => u.Id == testRecordId);

            //Act
            controller.Delete(testRecordId);

            //Assert
            mock.Verify(r => r.Remove<Record>(record));
        }

        private IQueryable<Record> GetTestRecords()
        {
            Equipment mob = new() { Id = 1, Name = "Mob", Value = 300 };
            Equipment pan = new() { Id = 2, Name = "Pan", Value = 500 };

            User petro = new() { Id = 1, Name = "Petro", Position = "Cleaner" };
            User ostap = new() { Id = 1, Name = "Ostap", Position = "Cook" };

            return new List<Record> {
                new() { Id = 1, Sender = petro, Receiver = ostap, Equipment = mob },
                new() { Id = 2, Sender = petro, Receiver = ostap, Equipment = pan },
                new() { Id = 3, Sender = ostap, Receiver = petro, Equipment = mob }
            }.AsQueryable();
        }

    }
}
