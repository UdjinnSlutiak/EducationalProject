// <copyright file="EquipmentLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;

    /// <summary>
    /// Realization of IEquipmentLogic interface. Part of repository pattern.
    /// </summary>
    public class EquipmentLogic : IEquipmentLogic
    {
        /// <summary>
        /// Variable uses to have access to equipment repository.
        /// </summary>
        private IRepository<Equipment> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentLogic"/> class.
        /// Receives IEquipmentRepository instance by dependency injection to work with equipment repository.
        /// </summary>
        /// <param name="repository">IEquipmentRepository instance received by dependency injection.</param>
        public EquipmentLogic(IRepository<Equipment> repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Equipment>> GetEquipmentsAsync(int offset, int count)
        {
            return await this.repository.GetAsync(offset, count);
        }

        /// <inheritdoc/>
        public async Task<Equipment> GetEquipmentByIdAsync(int id)
        {
            return await this.repository.GetAsync(id);
        }

        /// <inheritdoc/>
        public async Task CreateEquipmentAsync(Equipment equipment)
        {
            await this.repository.CreateAsync(equipment);
        }

        /// <inheritdoc/>
        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            await this.repository.UpdateAsync(equipment);
        }

        /// <inheritdoc/>
        public async Task DeleteEquipmentAsync(int id)
        {
            await this.repository.DeleteAsync(id);
        }
    }
}
