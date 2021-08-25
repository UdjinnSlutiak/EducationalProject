using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentControll.Domain.Models.BindingTargets
{
    /// <summary>
    /// This class prevents over-binding of User model class
    /// </summary>
    public class UserBindingTarget
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        public User ToUser()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Role = Role
            };
        }
    }
}
