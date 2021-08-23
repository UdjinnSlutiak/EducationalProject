// <copyright file="User.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model class that describes User object
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets User object Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets User object last name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets User object first name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets User object username.
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets User object password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets User object Position.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 4)]
        public string Role { get; set; }

        /// <summary>
        /// Overrided base Equals method.
        /// </summary>
        /// <param name="obj">Object instance to compare with.</param>
        /// <returns>Boolean comparison result. True if equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            if (obj is User user)
            {
                return this.Equals(user);
            }

            return false;
        }

        /// <summary>
        /// Overload of overrided base Equals method that receives User instance as parameter.
        /// </summary>
        /// <param name="user">User instance to compare with.</param>
        /// <returns>Boolean comparison result. True if equal, false if not.</returns>
        public bool Equals(User user)
        {
            return this.Id == user.Id;
        }

        /// <summary>
        /// Overrided base GetHashCode method.
        /// </summary>
        /// <returns>Integer Hash Code.</returns>
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
