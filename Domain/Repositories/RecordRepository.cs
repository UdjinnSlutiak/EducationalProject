// // <copyright file="RecordRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Realization of IRecordRepository interface. Part of repository pattern.
    /// </summary>
    public class RecordRepository : IRecordRepository
    {
        /// <summary>
        /// Variable uses to have access to database.
        /// </summary>
        private readonly ProjectContext context;

        /// <summary>
        /// Initializes a new instance of the RecordRepository class.
        /// Receives IProjectContext instance by dependency injection to work with database.
        /// </summary>
        /// <param name="context">IProjectContext instance received by dependency injection.</param>
        public RecordRepository(ProjectContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Realization of IRecordRepository Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Record ToString strings.</returns>
        public IEnumerable<Record> Get()
        {
            List<Record> records = this.context.Records.Include(r => r.Sender)
                .Include(r => r.Receiver).Include(r => r.Equipment).ToList();
            return records;
        }

        /// <summary>
        /// Realization of IEquipmentRepository overloaded Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>String of found Record instance ToString method.</returns>
        public Record Get(int id)
        {
            Record record = this.context.Records.Include(r => r.Sender).Include(r => r.Receiver)
                .Include(r => r.Equipment).FirstOrDefault(r => r.Id == id);
            return record;
        }

        /// <summary>
        /// Realization of IRecordRepository Create method.
        /// </summary>
        /// <param name="record">Record instance that contains SenderId, ReceiverId and EquipmentId parsed from JSON request.</param>
        public void Create(Record record)
        {
            this.context.Add(record);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Realization of IRecordRepository Update method.
        /// </summary>
        /// <param name="partialrecord">'Partial' Record instance that contains information to update.</param>
        public void Update(Record record)
        {
            this.context.Update(record);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Realization of IRecordRepository Delete method.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        public void Delete(int id)
        {
            this.context.Remove(new Record { Id = id });
            this.context.SaveChanges();
        }
    }
}
