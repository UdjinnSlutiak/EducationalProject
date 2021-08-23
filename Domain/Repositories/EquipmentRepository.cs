// <copyright file="EquipmentRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Realization of IEquipmentRepository interface. Part of repository pattern.
    /// </summary>
    public class EquipmentRepository : IEquipmentRepository
    {
        /// <summary>
        /// Variable uses to have access to database.
        /// </summary>
        private readonly ProjectContext context;

        /// <summary>
        /// Initializes a new instance of the EquipmentRepository class.
        /// Receives IProjectContext instance by dependency injection to work with database.
        /// </summary>
        /// <param name="context">IProjectContext instance received by dependency injection</param>
        public EquipmentRepository(ProjectContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Realization of IEquipmentRepository Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Equipment inastances.</returns>
        public IEnumerable<Equipment> Get()
        {
            return this.context.Equipments.ToList();
        }

        /// <summary>
        /// Realization of IEquipmentRepository overloaded Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Equipment instance.</returns>
        public Equipment Get(int id)
        {
            return this.context.Equipments.Find(id);
        }

        /// <summary>
        /// Realization of IEquipmentRepository Create method.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        public void Create(Equipment equipment)
        {
            this.context.Add(equipment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Realization of IEquipmentRepository Update method.
        /// </summary>
        /// <param name="id">Equipment to update Id value.</param>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        public void Update(Equipment equipment)
        {
            this.context.Update(equipment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Realization of IEquipmentRepository Delete method.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        public void Delete(int id)
        {
            this.context.Remove(new Equipment { Id = id });
            this.context.SaveChanges();
        }
    }
}
