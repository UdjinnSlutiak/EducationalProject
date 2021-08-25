﻿using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EquipmentControll.Web.Services
{
    public class JwtAuthService
    {
        private readonly JwtTokenConfig tokenConfig;
        private readonly ILogger<JwtAuthService> logger;

        public JwtAuthService(JwtTokenConfig tokenConfig)
        {
            this.tokenConfig = tokenConfig;
        }

        public string BuildToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.tokenConfig.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: this.tokenConfig.Issuer,
                    audience: this.tokenConfig.Audience,
                    notBefore: DateTime.Now,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(this.tokenConfig.AccessTokenExpiration),
                    signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string BuildRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenValidator = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.tokenConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            try
            {
                var principal = tokenValidator.ValidateToken(token, parameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken)
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                            StringComparison.InvariantCultureIgnoreCase))
                {
                    this.logger.LogError($"Token validation failed");
                    return null;
                }

                return principal;
            }
            catch (Exception e)
            {
                this.logger.LogError($"Token validation failed: {e.Message}");
                return null;
            }
        }
    }
}
