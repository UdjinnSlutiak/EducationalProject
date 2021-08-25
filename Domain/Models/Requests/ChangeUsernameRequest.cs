using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models.Requests
{
    public class ChangeUsernameRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$")]
        public string Username { get; set; }
    }
}
