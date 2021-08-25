using EquipmentControll.Domain.Models.Dto;

namespace EquipmentControll.Domain.Models.Responses
{
    public class RegisterResponse
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
