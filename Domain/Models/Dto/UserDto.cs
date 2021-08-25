namespace EquipmentControll.Domain.Models.Dto
{
    public class UserDto
    {
        public UserDto() { }

        public UserDto(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Role = user.Role;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
