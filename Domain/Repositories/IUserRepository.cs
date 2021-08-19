// <copyright file="IUserRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary User CRUD methods.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Method to get Users from Context.
        /// </summary>
        /// <returns>IEnumerable collection of User instances.</returns>
        public IEnumerable<User> Get();

        /// <summary>
        /// Overrided Get method to get User by Id from Context.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>Found User instance.</returns>
        public User Get(int id);

        /// <summary>
        /// Method to Create User
        /// </summary>
        /// <param name="user">User instance to add to database.</param>
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
