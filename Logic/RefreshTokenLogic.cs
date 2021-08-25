using EquipmentControll.Domain.Models;
using EquipmentControll.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentControll.Logic
{
    /// <summary>
    /// Describes all necessary refresh tokens logic CRUD methods.
    /// </summary>
    public class RefreshTokenLogic : IRefreshTokenLogic
    {
        private readonly IRepository<RefreshToken> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenLogic"/> class.
        /// Receives IRepository instance by dependency injection to work with refresh token repository.
        /// </summary>
        /// <param name="repository">IRepository instance received by dependency injection.</param>
        public RefreshTokenLogic(IRepository<RefreshToken> repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            await this.repository.CreateAsync(refreshToken);
        }

        /// <inheritdoc/>
        public async Task DeleteRefreshTokenAsync(string token)
        {
            await this.repository.DeleteAsync(new RefreshToken { Token = token });
        }

        /// <inheritdoc/>
        public async Task DeleteUserRefreshTokenInfosAsync(int userId)
        {
            var userRefreshTokens = await this.GetUserRefreshTokenInfosAsync(userId);
            await this.repository.DeleteRangeAsync(userRefreshTokens);
        }

        /// <inheritdoc/>
        public async Task<RefreshToken> GetRefreshTokenInfoAsync(string token)
        {
            return await this.repository.GetAsync(token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync(int offset, int count)
        {
            return await this.repository.GetAsync(offset, count);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RefreshToken>> GetUserRefreshTokenInfosAsync(int userId)
        {
            var userRefreshTokens = await this.repository
                .FilterAsync(token => token.UserId == userId);
            return userRefreshTokens;
        }

        /// <inheritdoc/>
        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            await this.repository.UpdateAsync(refreshToken);
        }
    }
}
