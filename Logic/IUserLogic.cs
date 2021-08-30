// <copyright file="IUserLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary User logic CRUD methods.
    /// </summary>
    public interface IUserLogic
    {
        /// <summary>
        /// Method to get Users from database.
        /// </summary>
        /// <param name="offset">Count of Users to skip.</param>
        /// <param name="count">Count of Users to take.</param>
        /// <returns>IEnumerable collection of User instances.</returns>
        public Task<IEnumerable<User>> GetUsersAsync(int offset, int count);

        /// <summary>
        /// Overrided Get method to get User by Id from database.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>Found User instance.</returns>
        public Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Method to Create User.
        /// </summary>
        /// <param name="user">User instance to add database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateUserAsync(User user);

        /// <summary>
        /// Method to update User.
        /// </summary>
        /// <param name="user">User instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateUserAsync(User user);

        /// <summary>
        /// Method to delete User.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteUserAsync(int id);
    }
}
