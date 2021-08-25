using EquipmentControll.Domain.Models;
using EquipmentControll.Domain.Models.Dto;
using EquipmentControll.Logic;
using EquipmentControll.Logic.Hashing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentControll.Web.Services
{
    public class AuthManager
    {
        private const string ClaimIdType = "id";
        private readonly ILogger<AuthManager> logger;
        private readonly IPasswordHasher passwordHasher;
        private readonly IRefreshTokenLogic refreshTokenLogic;
        private readonly IUserLogic userLogic;
        private readonly IBlacklistedTokenLogic blacklistedTokenLogic;
        private readonly JwtAuthService jwtAuthService;
        private readonly JwtTokenConfig jwtTokenConfig;

        public AuthManager(ILogger<AuthManager> logger, IPasswordHasher passwordHasher,
            JwtAuthService jwtAuthService, JwtTokenConfig jwtTokenConfig,
            IRefreshTokenLogic refreshTokenLogic, IBlacklistedTokenLogic blacklistedTokenLogic, IUserLogic userLogic)
        {
            this.logger = logger;
            this.passwordHasher = passwordHasher;
            this.refreshTokenLogic = refreshTokenLogic;
            this.userLogic = userLogic;
            this.blacklistedTokenLogic = blacklistedTokenLogic;
            this.jwtAuthService = jwtAuthService;
            this.jwtTokenConfig = jwtTokenConfig;
        }

        public async Task<SignInResult> SignIn(string username, string password)
        {
            this.logger.LogInformation($"Validating user [{username}]", username);
            SignInResult result = new SignInResult();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return result;
            }

            string passwordHash = this.passwordHasher.Hash(password);
            User user = await this.userLogic.GetUserForLoginAsync(username, passwordHash);
            if (user != null)
            {
                var claims = this.BuildClaims(user);
                result.User = new UserDto(user);
                result.AccessToken = this.jwtAuthService.BuildToken(claims);
                result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

                await this.refreshTokenLogic.CreateRefreshTokenAsync(new RefreshToken
                {
                    UserId = user.Id,
                    Token = result.RefreshToken,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration) 
                });

                result.Success = true;
            }

            return result;
        }

        public async Task<SignInResult> RefreshToken(string accessToken, string refreshToken)
        {
            ClaimsPrincipal claimsPrincipal = this.jwtAuthService.GetPrincipalFromToken(accessToken);
            SignInResult result = new SignInResult();

            if (claimsPrincipal == null)
            {
                return result;
            }

            string id = claimsPrincipal.Claims.First(c => c.Type == ClaimIdType).Value;
            var user = await this.userLogic.GetUserByIdAsync(Convert.ToInt32(id));
            if (user == null)
            {
                return result;
            }

            RefreshToken token = await this.refreshTokenLogic.GetRefreshTokenInfoAsync(refreshToken);
            if (token == null || DateTime.Now > token.ExpiresAt)
            {
                return result;
            }

            var claims = this.BuildClaims(user);

            result.User = new UserDto(user);
            result.AccessToken = this.jwtAuthService.BuildToken(claims);
            result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

            await this.refreshTokenLogic.DeleteRefreshTokenAsync(token.Token);
            await this.refreshTokenLogic.CreateRefreshTokenAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = result.RefreshToken,
                IssuedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration) 
            });

            result.Success = true;
            return result;
        }

        private Claim[] BuildClaims(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimIdType, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            return claims;
        }

        public async Task Logout(HttpContext context)
        {
            string token = this.jwtAuthService.GetCurrentAccessTokenFromHeader(context);
            if (token != null)
            {
                await this.blacklistedTokenLogic.AddBlacklistedTokenAsync(new BlacklistedToken
                {
                    AccessToken = token,
                    ExpirationDate = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration)
                });
                this.jwtAuthService.RemoveAuthHeader(context);
            }
        }
    }
}
