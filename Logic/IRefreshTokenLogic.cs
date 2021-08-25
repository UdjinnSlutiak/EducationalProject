using EquipmentControll.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentControll.Logic
{
    /// <summary>
    /// Describes all necessary refresh tokens logic CRUD methods.
    /// </summary>
    public interface IRefreshTokenLogic
    {
        /// <summary>
        /// Method to get refresh tokens from database.
        /// </summary>
        /// <param name="offset">Count of RefreshToken to skip.</param>
        /// <param name="count">Count of RefreshToken to take.</param>
        /// <returns>IEnumerable collection of User instances.</returns>
        public Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync(int offset, int count);

        /// <summary>
        /// Method to get additional info about refresh token.
        /// </summary>
        /// <param name="token">Token to find info about.</param>
        /// <returns>Found User instance.</returns>
        public Task<RefreshToken> GetRefreshTokenInfoAsync(string token);

        /// <summary>
        /// Method to Create refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token instance to add database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Method to update refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Method to delete refresh token.
        /// </summary>
        /// <param name="token">Refresh token to delete info about.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteRefreshTokenAsync(string token);

        /// <summary>
        /// Gets all refresh token infos created by this user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IEnumerable<RefreshToken>> GetUserRefreshTokenInfosAsync(int userId);

        /// <summary>
        /// Delets all refresh token infos created by this user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task DeleteUserRefreshTokenInfosAsync(int userId);
    }
}
