// <copyright file="IRecordLogic.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary Record logic CRUD methods.
    /// </summary>
    public interface IRecordLogic
    {
        /// <summary>
        /// Method to get Records from Context.
        /// </summary>
        /// <returns>IEnumerable collection of Record ToString strings.</returns>
        public IEnumerable<string> Get();

        /// <summary>
        /// Overrided Get method to get Record by Id from Context.
        /// </summary>
        /// <param name="id">Record to find Id value.</param>
        /// <returns>String of found Record instance ToString method.</returns>
        public string Get(int id);

        /// <summary>
        /// Method to create Record.
        /// </summary>
        /// <param name="record">Record instance to add to database.</param>
        public void Create(Record record);

        /// <summary>
        /// Method to update Record.
        /// </summary>
        /// <param name="id">Record to update Id value.</param>
        /// <param name="record">Record instance that contains information to update.</param>
        public void Update(int id, Record record);

        /// <summary>
        /// Method to delete Record.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        public void Delete(int id);
    }
}
