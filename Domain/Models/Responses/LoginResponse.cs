namespace EquipmentControll.Domain.Models.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
