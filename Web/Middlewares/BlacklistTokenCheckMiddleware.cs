using EquipmentControll.Logic;
using EquipmentControll.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EquipmentControll.Web.Middlewares
{
    public class BlacklistTokenCheckMiddleware
    {
        private readonly ILogger<BlacklistTokenCheckMiddleware> logger;
        private readonly RequestDelegate next;
        private readonly IBlacklistedTokenLogic blacklistedTokenLogic;
        private readonly JwtAuthService jwtAuthService;

        public BlacklistTokenCheckMiddleware(ILogger<BlacklistTokenCheckMiddleware> logger, RequestDelegate next,
            IBlacklistedTokenLogic blacklistedTokenLogic, JwtAuthService jwtAuthService)
        {
            this.logger = logger;
            this.next = next;
            this.blacklistedTokenLogic = blacklistedTokenLogic;
            this.jwtAuthService = jwtAuthService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = this.jwtAuthService.GetCurrentAccessTokenFromHeader(context);
            bool isTokenBlacklisted = await this.blacklistedTokenLogic.IsTokenBlacklistedAsync(token);

            if (isTokenBlacklisted)
            {
                this.jwtAuthService.RemoveAuthHeader(context);
                this.logger.LogInformation("Handled request from blacklisted token.");
            }

            if (this.next != null)
            {
                await this.next(context);
            }
        }
    }
}
