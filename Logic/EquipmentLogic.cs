// <copyright file="EquipmentLogic.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
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
        private IEquipmentRepository repository;

        /// <summary>
        /// Initializes a new instance of the EquipmentLogic class.
        /// Receives IEquipmentRepository instance by dependency injection to work with equipment repository.
        /// </summary>
        /// <param name="repository">IEquipmentRepository instance received by dependency injection.</param>
        public EquipmentLogic(IEquipmentRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Realization of IEquipmentLogic Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Equipment inastances.</returns>
        public IEnumerable<Equipment> Get()
        {
            return this.repository.Get();
        }

        /// <summary>
        /// Realization of IEquipmentLogic overloaded Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Equipment instance.</returns>
        public Equipment Get(int id)
        {
            return this.repository.Get(id);
        }

        /// <summary>
        /// Realization of IEquipmentLogic Create method.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        public void Create(Equipment equipment)
        {
            this.repository.Create(equipment);
        }

        /// <summary>
        /// Realization of IEquipmentLogic Update method.
        /// </summary>
        /// <param name="id">Equipment to update Id value.</param>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        public void Update(Equipment equipment)
        {
            this.repository.Update(equipment);
        }

        /// <summary>
        /// Realization of IEquipmentRepository Delete method.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }
    }
}
