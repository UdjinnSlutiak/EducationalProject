using EquipmentControll.Domain.Models.Requests;
using EquipmentControll.Domain.Models.Responses;
using EquipmentControll.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentControll.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager signInManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await this.signInManager.SignIn(request.Username, request.Password);

            if (!result.Success)
            {
                return this.Unauthorized();
            }

            this.logger.LogInformation($"User [{request.Username}] logged in the system.");

            return this.Ok(new LoginResponse
            {
                Username = result.User.Username,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var result = await this.signInManager.RefreshToken(request.AccessToken, request.RefreshToken);

            if (!result.Success)
            {
                return this.Unauthorized();
            }

            this.logger.LogInformation($"User [{result.User.Username}] refreshed token in the system.");

            return this.Ok(new LoginResponse
            {
                Username = result.User.Username,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }

        [HttpGet("refresh-tokens")]
        public async Task<ActionResult> RefreshTokens()
        {
            int? userId = this.GetCurrentUserId();

            if (userId == null)
            {
                return this.Unauthorized();
            }

            return this.Ok();
        }

        private int? GetCurrentUserId()
        {
            string id = this.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "id")?.Value;

            return string.IsNullOrEmpty(id) ? null : int.Parse(id);
        }
    }
}
