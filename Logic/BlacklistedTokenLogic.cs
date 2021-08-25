using EquipmentControll.Domain.Models;
using EquipmentControll.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace EquipmentControll.Logic
{
    public class BlacklistedTokenLogic : IBlacklistedTokenLogic
    {
        private readonly IRepository<BlacklistedToken> repository;

        public BlacklistedTokenLogic(IRepository<BlacklistedToken> repository)
        {
            this.repository = repository;
        }

        public async Task AddBlacklistedTokenAsync(BlacklistedToken blacklistedToken)
        {
            await this.repository.CreateAsync(blacklistedToken);
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return await this.repository.GetAsync(token) != null;
        }

        public async Task RemoveBlacklistedTokenAsync(BlacklistedToken blacklistedToken)
        {
            await this.repository.DeleteAsync(blacklistedToken);
        }

        public async Task RemoveExpiredTokensAsync()
        {
            var expiredTokens = await this.repository.FilterAsync(bt => DateTime.Now >= bt.ExpirationDate);
            await this.repository.DeleteRangeAsync(expiredTokens);
        }
    }
}
