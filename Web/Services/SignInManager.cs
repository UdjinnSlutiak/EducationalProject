using EquipmentControll.Domain.Models;
using EquipmentControll.Domain.Models.Dto;
using EquipmentControll.Logic.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentControll.Web.Services
{
    public class SignInManager
    {
        private const string ClaimIdType = "id";
        private readonly ILogger<SignInManager> logger;
        private readonly IPasswordHasher passwordHasher;
        private readonly ProjectContext ctx;
        private readonly JwtAuthService jwtAuthService;
        private readonly JwtTokenConfig jwtTokenConfig;

        public SignInManager(ILogger<SignInManager> logger, IPasswordHasher passwordHasher,
            JwtAuthService jwtAuthService, JwtTokenConfig jwtTokenConfig, ProjectContext ctx)
        {
            this.logger = logger;
            this.passwordHasher = passwordHasher;
            this.ctx = ctx;
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
            User user = await this.ctx.Users.Where(f => f.Username == username
                && f.PasswordHash == passwordHash).FirstOrDefaultAsync();
            if (user != null)
            {
                var claims = this.BuildClaims(user);
                result.User = new UserDto(user);
                result.AccessToken = this.jwtAuthService.BuildToken(claims);
                result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

                this.ctx.RefreshTokens.Add(new RefreshToken 
                {
                    UserId = user.Id,
                    Token = result.RefreshToken,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration) 
                });

                await this.ctx.SaveChangesAsync();
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
            var user = await this.ctx.Users.FindAsync(Convert.ToInt32(id));

            if (user == null)
            {
                return result;
            }

            RefreshToken token = await this.ctx.RefreshTokens
                    .Where(f => f.UserId == user.Id
                            && f.Token == refreshToken
                            && f.ExpiresAt >= DateTime.Now)
                    .FirstOrDefaultAsync();

            if (token == null)
            {
                return result;
            }

            var claims = this.BuildClaims(user);

            result.User = new UserDto(user);
            result.AccessToken = this.jwtAuthService.BuildToken(claims);
            result.RefreshToken = this.jwtAuthService.BuildRefreshToken();

            this.ctx.RefreshTokens.Remove(token);
            this.ctx.RefreshTokens.Add(new RefreshToken 
            {
                UserId = user.Id,
                Token = result.RefreshToken,
                IssuedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(this.jwtTokenConfig.RefreshTokenExpiration) 
            });
            await this.ctx.SaveChangesAsync();

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
    }
}
