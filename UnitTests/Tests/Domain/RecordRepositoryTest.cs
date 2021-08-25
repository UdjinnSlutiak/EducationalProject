// <copyright file="RecordRepositoryTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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
    using Record = EquipmentControll.Domain.Models.Record;

    /// <summary>
    /// xUnit Test class.
    /// </summary>
    public class RecordRepositoryTest
    {
        /// <summary>
        /// Tests if RecordRepository Get method returns Records collection correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetReturnsListOfRecords()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);

            // Act
            var result = await controller.GetAsync(0, 3);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            _ = result.Contains(this.GetTestRecords().First());
        }

        /// <summary>
        /// Tests if RecordRepository Get method returns Record instance by Id correctly.
        /// </summary>
        [Fact]
        public void GetByIdReturnsRecord()
        {
            // Arrange
            var testRecordId = 3;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);

            // Act
            var result = controller.GetAsync(testRecordId);

            // Assert
            Assert.NotNull(result);
            result.Equals(this.GetTestRecords().First(record => record.Id == testRecordId));
        }

        /// <summary>
        /// Tests if RecordRepository Create method adds Record instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task CreateRecordAddsRecord()
        {
            // Arrange
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Record>(context);
            var record = this.GetTestRecords().First();
            record.SenderId = record.Sender.Id;
            record.ReceiverId = record.Receiver.Id;
            record.EquipmentId = record.Equipment.Id;

            // Act
            await controller.CreateAsync(record);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Records.Contains(this.GetTestRecords().First()));
        }

        /// <summary>
        /// Tests if RecordRepository Update method changes Record instance information correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task UpdateRecordChangesInformation()
        {
            // Arrange
            var testRecordId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            var controller = new Repository<Record>(context);
            var record = this.GetTestRecords().First(u => u.Id == testRecordId);

            // Act
            record.IsReturned = true;
            await controller.UpdateAsync(record);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(context.Records.Where(e => e.Id == testRecordId).First().IsReturned == true);
        }

        /// <summary>
        /// Tests if RecordRepository Delete method removes Record instance correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task DeleteRecordRemovesRecord()
        {
            // Arrange
            var testRecordId = 1;
            var context = new ProjectContext("Server=localhost,1433; Database=TestProjectDB; User=sa; Password=KAnITOWKA13");
            context.AddRange(this.GetTestRecords());
            var controller = new Repository<Record>(context);
            var record = this.GetTestRecords().First(u => u.Id == testRecordId);

            // Act
            await controller.DeleteAsync(testRecordId);
            await context.SaveChangesAsync();

            // Assert
            Assert.False(context.Records.Contains(record));
        }

        /// <summary>
        /// Creates Records collection.
        /// </summary>
        /// <returns>IQueryable Records List as IQueryable.</returns>
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
