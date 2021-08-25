using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models
{
    public class BlacklistedToken
    {
        [Key]
        public string AccessToken { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
