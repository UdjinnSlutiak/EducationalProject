using EquipmentControll.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models.Requests
{
    public class ChangeUserRoleRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringRange(AllowableValues = new[] { "Administrator", "Moderator", "User" })]
        public string Role { get; set; }
    }
}
