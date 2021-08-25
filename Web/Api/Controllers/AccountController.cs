using System.Linq;
using System.Threading.Tasks;
using EquipmentControll.Domain.Models;
using EquipmentControll.Domain.Models.Dto;
using EquipmentControll.Domain.Models.Requests;
using EquipmentControll.Domain.Models.Responses;
using EquipmentControll.Logic;
using EquipmentControll.Logic.Hashing;
using EquipmentControll.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EquipmentControll.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly AuthManager authManager;
        private readonly IRefreshTokenLogic refreshTokenLogic;
        private readonly IUserLogic userLogic;
        private readonly IPasswordHasher passwordHasher;
        private readonly JwtAuthService jwtAuthService;

        public AccountController(ILogger<AccountController> logger, AuthManager authManager,
            IRefreshTokenLogic refreshTokenLogic, IUserLogic userLogic, IPasswordHasher passwordHasher)
        {
            this.logger = logger;
            this.authManager = authManager;
            this.refreshTokenLogic = refreshTokenLogic;
            this.userLogic = userLogic;
            this.jwtAuthService = jwtAuthService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var signInResult = await this.authManager.SignIn(request.Username, request.Password);

            if (!signInResult.Success)
            {
                return this.Unauthorized();
            }

            this.logger.LogInformation($"User [{request.Username}] logged in the system.");

            return this.Ok(new LoginResponse
            {
                Username = signInResult.User.Username,
                AccessToken = signInResult.AccessToken,
                RefreshToken = signInResult.RefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            bool isUsernameAvailable = await userLogic.IsUsernameAvailableAsync(request.Username);
            if (!isUsernameAvailable)
            {
                this.ModelState.AddModelError(string.Empty, "This username has already taken.");
                return this.ValidationProblem();
            }

            string passwordHash = this.passwordHasher.Hash(request.Password);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                PasswordHash = passwordHash,
                Role = "User"
            };
            await this.userLogic.CreateUserAsync(user);
            var signInResult = await this.authManager.SignIn(user.Username, request.Password);

            this.logger.LogInformation($"User [{request.Username}] registered in the system.");

            return this.Ok(new RegisterResponse
            {
                User = new UserDto(user),
                AccessToken = signInResult.AccessToken,
                RefreshToken = signInResult.RefreshToken
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var result = await this.authManager.RefreshToken(request.AccessToken, request.RefreshToken);

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

            var userRefreshTokenDtos = (await this.refreshTokenLogic
                .GetUserRefreshTokenInfosAsync(userId.Value))
                .Select(rt => new RefreshTokenDto(rt));

            return this.Ok(userRefreshTokenDtos);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            int? userId = this.GetCurrentUserId();

            if (userId == null)
            {
                return this.Unauthorized();
            }

            await this.userLogic.ChangePasswordAsync(userId.Value, request.Password);
            await this.authManager.Logout(this.HttpContext);
            return this.Ok();
        }

        [HttpPost("change-username")]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameRequest request)
        {
            int? userId = this.GetCurrentUserId();

            if (userId == null)
            {
                return this.Unauthorized();
            }

            bool isUsernameAvailable = await this.userLogic.IsUsernameAvailableAsync(request.Username);
            if (!isUsernameAvailable)
            {
                this.ModelState.AddModelError(string.Empty, "This username has already taken.");
                return this.ValidationProblem();
            }

            await this.userLogic.ChangeUsernameAsync(userId.Value, request.Username);
            await this.authManager.Logout(this.HttpContext);

            return this.Ok();
        }

        [HttpPost("change-name")]
        public async Task<IActionResult> ChangeName(ChangeNameRequest request)
        {
            int? userId = this.GetCurrentUserId();

            if (userId == null)
            {
                return this.Unauthorized();
            }

            await this.userLogic.ChangeNameAsync(userId.Value, request.FirstName, request.LastName);

            return this.Ok();
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.authManager.Logout(this.HttpContext);
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
