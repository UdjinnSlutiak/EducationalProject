﻿// <copyright file="IEquipmentRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary Equipment CRUD methods.
    /// </summary>
    public interface IEquipmentRepository
    {
        /// <summary>
        /// Method to get Equipments from Context.
        /// </summary>
        /// <returns>IEnumerable collection of Equipment instances.</returns>
        public IEnumerable<Equipment> Get();

        /// <summary>
        /// Overrided Get method to get Equipment by Id from Context.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Found Equipment instance.</returns>
        public Equipment Get(int id);

        /// <summary>
        /// Method to create Equipment.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        public void Create(Equipment equipment);

        /// <summary>
        /// Method to update Equipment.
        /// </summary>
        /// <param name="id">Equipment to update Id value.</param>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        public void Update(int id, Equipment equipment);

        /// <summary>
        /// Method to delete Equipment.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        public void Delete(int id);
    }
}
