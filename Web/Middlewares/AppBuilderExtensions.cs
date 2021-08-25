using Microsoft.AspNetCore.Builder;

namespace EquipmentControll.Web.Middlewares
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseBlacklistTokenCheck(this IApplicationBuilder app)
        {
            app.UseMiddleware<BlacklistTokenCheckMiddleware>();
            return app;
        }
    }
}
