using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
