// <copyright file="UserLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using EquipmentControll.Logic.Hashing;

    /// <summary>
    /// Realization of IUserLogic interface. Part of repository pattern.
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// Variable uses to have access to user repository.
        /// </summary>
        private readonly IRepository<User> repository;
        private readonly IRefreshTokenLogic refreshTokenLogic;
        private readonly IPasswordHasher passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogic"/> class.
        /// Receives IUserRepository instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="repository">IUserRepository instance received by dependency injection.</param>
        /// <param name="refreshTokenLogic">IRefreshTokenLogic instance received by dependency injection.</param>
        public UserLogic(IRepository<User> repository, IRefreshTokenLogic refreshTokenLogic,
            IPasswordHasher passwordHasher)
        {
            this.repository = repository;
            this.refreshTokenLogic = refreshTokenLogic;
            this.passwordHasher = passwordHasher;
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
        public async Task<User> GetUserForLoginAsync(string username, string passwordHash)
        {
            return (await this.repository.FilterAsync(user => user.Username == username
                && user.PasswordHash == passwordHash)).FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return (await this.repository.FilterAsync(user =>
                user.Username == username)).Any() == false;
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
            await this.repository.DeleteAsync(new User { Id = id });
        }

        public async Task ChangeUserRoleAsync(int userId, string role)
        {
            User user = await this.GetUserByIdAsync(userId);
            user.Role = role;
            await this.UpdateUserAsync(user);
            await this.refreshTokenLogic.DeleteUserRefreshTokenInfosAsync(userId);
        }

        public async Task ChangePasswordAsync(int userId, string password)
        {
            string passwordHash = this.passwordHasher.Hash(password);
            User user = await this.GetUserByIdAsync(userId);
            user.PasswordHash = passwordHash;
            await this.UpdateUserAsync(user);
            await this.refreshTokenLogic.DeleteUserRefreshTokenInfosAsync(userId);
        }

        public async Task ChangeUsernameAsync(int userId, string username)
        {
            User user = await this.GetUserByIdAsync(userId);
            user.Username = username;
            await this.UpdateUserAsync(user);
            await this.refreshTokenLogic.DeleteUserRefreshTokenInfosAsync(userId);
        }

        public async Task ChangeNameAsync(int userId, string firstName, string lastName)
        {
            User user = await this.GetUserByIdAsync(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            await this.UpdateUserAsync(user);
        }
    }
}
