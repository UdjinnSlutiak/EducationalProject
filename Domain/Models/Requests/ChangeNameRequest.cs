using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models.Requests
{
    public class ChangeNameRequest
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }
    }
}
