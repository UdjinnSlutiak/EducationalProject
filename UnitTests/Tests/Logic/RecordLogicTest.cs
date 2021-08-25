// <copyright file="RecordLogicTest.cs" company="Eugene Slutiak">
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
    using Record = EquipmentControll.Domain.Models.Record;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class RecordLogicTest
    {
        /// <summary>
        /// Tests if RecordLogic Get method returns Record collection correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfRecords()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);
            var logic = new RecordLogic(controller);

            // Act
            var result = await logic.GetRecordsAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(this.GetTestRecords().First());
        }

        /// <summary>
        /// Tests if RecordLogic Get method returns Record instance by Id correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdReturnsRecord()
        {
            // Arrange
            int testEquipmentId = 2;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);
            var logic = new RecordLogic(controller);

            // Act
            var result = await logic.GetRecordByIdAsync(testEquipmentId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            result.Equals(this.GetTestRecords().FirstOrDefault(user => user.Id == testEquipmentId));
        }

        /// <summary>
        /// Tests if RecordLogic Create method adds Record instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateRecordAddsRecord()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Record>(context);
            var logic = new RecordLogic(controller);

            // Act
            await logic.CreateRecordAsync(this.GetTestRecords().First());
            await context.SaveChangesAsync();

            // Assert
            context.Records.Contains(this.GetTestRecords().First());
        }

        /// <summary>
        /// Tests if RecordLogic Update method changes Record instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateRecordChangesInformation()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);
            var logic = new RecordLogic(controller);
            Record record = this.GetTestRecords().First();

            // Act
            record.IsReturned = true;
            await logic.UpdateRecordAsync(record);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Records.First().IsReturned == true);
        }

        /// <summary>
        /// Tests if RecordLogic Delete method removes Record instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteRecordRemovesRecord()
        {
            // Arrange
            var testUserId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);
            var logic = new RecordLogic(controller);
            var record = this.GetTestRecords().First();

            // Act
            await logic.DeleteRecordAsync(testUserId);
            await context.SaveChangesAsync();

            // Assert
            Assert.False(context.Records.Contains(record));
        }

        /// <summary>
        /// Creates Records collection.
        /// </summary>
        /// <returns>Records List.</returns>
        private List<Record> GetTestRecords()
        {
            Equipment mob = new () { Id = 1, Name = "Mob", Price = 300 };
            Equipment pan = new () { Id = 2, Name = "Pan", Price = 500 };

            User petro = new () { Id = 1, FirstName = "Petro", LastName = "Mostavchuk", Role = "Motivator", PasswordHash = "hashed_12345", Username = "mc_petia" };
            User ostap = new () { Id = 2, FirstName = "Ostap", LastName = "Korobenko", Role = "Cook", PasswordHash = "hashed_23456", Username = "korobka" };

            return new List<Record>
            {
                new () { Id = 1, Sender = petro, Receiver = ostap, Equipment = mob, IsReturned = false, GivenDate = System.DateTime.Today, Deadline = System.DateTime.Today, EquipmentId = mob.Id, SenderId = petro.Id, ReceiverId = ostap.Id },
                new () { Id = 2, Sender = petro, Receiver = ostap, Equipment = pan, IsReturned = false, GivenDate = System.DateTime.Today, Deadline = System.DateTime.Today, EquipmentId = pan.Id, SenderId = petro.Id, ReceiverId = ostap.Id },
                new () { Id = 3, Sender = ostap, Receiver = petro, Equipment = mob, IsReturned = false, GivenDate = System.DateTime.Today, Deadline = System.DateTime.Today, EquipmentId = mob.Id, SenderId = ostap.Id, ReceiverId = petro.Id },
            };
        }
    }
}
