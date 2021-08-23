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
        private readonly IProjectContext context;

        /// <summary>
        /// Initializes a new instance of the RecordRepository class.
        /// Receives IProjectContext instance by dependency injection to work with database.
        /// </summary>
        /// <param name="context">IProjectContext instance received by dependency injection.</param>
        public RecordRepository(IProjectContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Realization of IRecordRepository Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Record ToString strings.</returns>
        public IEnumerable<string> Get()
        {
            List<Record> records = this.context.Records.Include(r => r.Sender).Include(r => r.Receiver).Include(r => r.Equipment).ToList();

            List<string> strings = new();

            foreach (var item in records)
            {
                strings.Add(item.ToString());
            }

            return strings;
        }

        /// <summary>
        /// Realization of IEquipmentRepository overloaded Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>String of found Record instance ToString method.</returns>
        public string Get(int id)
        {
            Record record = this.GetRecord(id);
            if (record != null)
            {
                return record.ToString();
            }
            else
            {
                return "Not Found";
            }
        }

        /// <summary>
        /// Realization of IRecordRepository Create method.
        /// </summary>
        /// <param name="partialRecord">'Partial' Record instance that contains SenderId, ReceiverId and EquipmentId parsed from JSON request.</param>
        public void Create(Record partialRecord)
        {
            Record record = this.CreateRecord(partialRecord.SenderId, partialRecord.ReceiverId, partialRecord.EquipmentId);
            this.context.Add(record);
            this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Realization of IRecordRepository Update method.
        /// </summary>
        /// <param name="id">Record to update Id value.</param>
        /// <param name="partialrecord">'Partial' Record instance that contains information to update.</param>
        public void Update(int id, Record partialrecord)
        {
            Record record = this.CreateRecord(partialrecord.SenderId, partialrecord.ReceiverId, partialrecord.EquipmentId);
            record.Id = id;
            this.context.Update(record);
            this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Realization of IRecordRepository Delete method.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        public void Delete(int id)
        {
            if (id > 0)
            {
                this.context.Remove(this.context.Records.FirstOrDefault(r => r.Id == id));
                this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Creates and returns created Record instance by received information.
        /// </summary>
        /// <param name="senderId">Id of user that sended equipment.</param>
        /// <param name="receiverId">Id of user that received equipment.</param>
        /// <param name="equipmentId">Id of given equipment.</param>
        /// <returns>Created Record instance.</returns>
        private Record CreateRecord(int senderId, int receiverId, int equipmentId)
        {
            User sender = this.context.Users.FirstOrDefault(r => r.Id == senderId);
            User receiver = this.context.Users.FirstOrDefault(r => r.Id == receiverId);
            Equipment equipment = this.context.Equipments.FirstOrDefault(r => r.Id == equipmentId);

            return new()
            {
                Sender = sender,
                Receiver = receiver,
                Equipment = equipment
            };
        }

        /// <summary>
        /// Searches Record instance in context by its Id value.
        /// </summary>
        /// <param name="id">Record Id value.</param>
        /// <returns>Found Record instance.</returns>
        private Record GetRecord(int id)
        {
            return this.context.Records.Include(r => r.Sender).Include(r => r.Receiver).Include(r => r.Equipment).FirstOrDefault(r => r.Id == id);
        }
    }
}
