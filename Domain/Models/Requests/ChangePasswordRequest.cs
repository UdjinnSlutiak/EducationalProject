using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
