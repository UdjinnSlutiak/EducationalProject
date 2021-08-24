// <copyright file="IEquipmentLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary Equipment logic CRUD methods.
    /// </summary>
    public interface IEquipmentLogic
    {
        /// <summary>
        /// Method to get Equipments.
        /// </summary>
        /// <param name="offset">Count of Equipments to skip.</param>
        /// <param name="count">Count of Equipments to take.</param>
        /// <returns>IEnumerable collection of Equipment instances.</returns>
        public Task<IEnumerable<Equipment>> GetEquipmentsAsync(int offset, int count);

        /// <summary>
        /// Overrided Get method to get Equipment by Id.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Found Equipment instance.</returns>
        public Task<Equipment> GetEquipmentByIdAsync(int id);

        /// <summary>
        /// Method to create Equipment.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateEquipmentAsync(Equipment equipment);

        /// <summary>
        /// Method to update Equipment.
        /// </summary>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateEquipmentAsync(Equipment equipment);

        /// <summary>
        /// Method to delete Equipment.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteEquipmentAsync(int id);
    }
}
