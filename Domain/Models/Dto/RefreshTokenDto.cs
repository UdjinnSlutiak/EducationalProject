using System;

namespace EquipmentControll.Domain.Models.Dto
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto() { }

        public RefreshTokenDto(RefreshToken refreshToken)
        {
            this.RefreshToken = refreshToken.Token;
            this.IssuedAt = refreshToken.IssuedAt;
            this.IsExpired = DateTime.Now < refreshToken.ExpiresAt;
        }

        public string RefreshToken { get; set; }

        public DateTime IssuedAt { get; set; }

        public bool IsExpired { get; set; }

    }
}
