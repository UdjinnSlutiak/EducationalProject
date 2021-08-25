// <copyright file="RecordLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        private IRepository<Record> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordLogic"/> class.
        /// Receives IRecordRepository instance by dependency injection to work with record repository.
        /// </summary>
        /// <param name="repository">IRecordRepository instance received by dependency injection.</param>
        public RecordLogic(IRepository<Record> repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Record>> GetRecordsAsync(int offset, int count)
        {
            return await this.repository.GetAsync(offset, count);
        }

        /// <inheritdoc/>
        public async Task<Record> GetRecordByIdAsync(int id)
        {
            return await this.repository.GetAsync(id);
        }

        /// <inheritdoc/>
        public async Task CreateRecordAsync(Record record)
        {
            await this.repository.CreateAsync(record);
        }

        /// <inheritdoc/>
        public async Task UpdateRecordAsync(Record record)
        {
            await this.repository.UpdateAsync(record);
        }

        /// <inheritdoc/>
        public async Task DeleteRecordAsync(int id)
        {
            await this.repository.DeleteAsync(new Record { Id = id });
        }
    }
}
