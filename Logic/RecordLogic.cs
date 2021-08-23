// <copyright file="RecordLogic.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;

    /// <summary>
    /// Realization of IRecordLogic interface. Part of repository pattern.
    /// </summary>
    public class RecordLogic : IRecordLogic
    {
        /// <summary>
        /// Variable uses to have access to record repository.
        /// </summary>
        private IRecordRepository repository;

        /// <summary>
        /// Initializes a new instance of the RecordLogic class.
        /// Receives IRecordRepository instance by dependency injection to work with record repository.
        /// </summary>
        /// <param name="repository">IRecordRepository instance received by dependency injection.</param>
        public RecordLogic(IRecordRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Realization of IRecordLogic Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Record ToString strings.</returns>
        public IEnumerable<string> Get()
        {
            return this.repository.Get();
        }

        /// <summary>
        /// Realization of IEquipmentLogic overloaded Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>String of found Record instance ToString method.</returns>
        public string Get(int id)
        {
            return this.repository.Get(id);
        }

        /// <summary>
        /// Realization of IRecordLogic Create method.
        /// </summary>
        /// <param name="record">'Partial' Record instance that contains SenderId, ReceiverId and EquipmentId parsed from JSON request.</param>
        public void Create(Record record)
        {
            this.repository.Create(record);
        }

        /// <summary>
        /// Realization of IRecordLogic Update method.
        /// </summary>
        /// <param name="id">Record to update Id value.</param>
        /// <param name="record">'Partial' Record instance that contains information to update.</param>
        public void Update(int id, Record record)
        {
            this.repository.Update(id, record);
        }

        /// <summary>
        /// Realization of IRecordLogic Delete method.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }
    }
}
