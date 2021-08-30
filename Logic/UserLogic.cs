// <copyright file="UserLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;

    /// <summary>
    /// Realization of IUserLogic interface. Part of repository pattern.
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// Variable uses to have access to user repository.
        /// </summary>
        private IRepository<User> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogic"/> class.
        /// Receives IUserRepository instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="repository">IUserRepository instance received by dependency injection.</param>
        public UserLogic(IRepository<User> repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetUsersAsync(int offset, int count)
        {
            return await this.repository.GetAsync(offset, count);
        }

        /// <inheritdoc/>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await this.repository.GetAsync(id);
        }

        /// <inheritdoc/>
        public async Task CreateUserAsync(User user)
        {
            await this.repository.CreateAsync(user);
        }

        /// <inheritdoc/>
        public async Task UpdateUserAsync(User user)
        {
            await this.repository.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            await this.repository.DeleteAsync(id);
        }
    }
}
