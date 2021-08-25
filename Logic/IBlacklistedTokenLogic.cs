using EquipmentControll.Domain.Models;
using System.Threading.Tasks;

namespace EquipmentControll.Logic
{
    public interface IBlacklistedTokenLogic
    {
        Task AddBlacklistedTokenAsync(BlacklistedToken blacklistedToken);
        Task RemoveBlacklistedTokenAsync(BlacklistedToken blacklistedToken);
        Task<bool> IsTokenBlacklistedAsync(string token);
        Task RemoveExpiredTokensAsync();
    }
}
