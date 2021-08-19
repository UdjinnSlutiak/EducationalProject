// <copyright file="IUserLogic.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary User logic CRUD methods.
    /// </summary>
    public interface IUserLogic
    {
        /// <summary>
        /// Method to get Users from database.
        /// </summary>
        /// <returns>IEnumerable collection of User instances.</returns>
        public IEnumerable<User> Get();

        /// <summary>
        /// Overrided Get method to get User by Id from database.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>Found User instance.</returns>
        public User Get(int id);

        /// <summary>
        /// Method to Create User
        /// </summary>
        /// <param name="user">User instance to add database.</param>
        public void Create(User user);

        /// <summary>
        /// Method to update User.
        /// </summary>
        /// <param name="id">User to update Id value.</param>
        /// <param name="user">User instance that contains information to update.</param>
        public void Update(int id, User user);

        /// <summary>
        /// Method to delete User.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        public void Delete(int id);
    }
}
