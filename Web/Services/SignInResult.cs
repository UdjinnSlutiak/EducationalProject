using EquipmentControll.Domain.Models.Dto;

namespace EquipmentControll.Web.Services
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}