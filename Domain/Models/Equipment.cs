// <copyright file="Equipment.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model class that describes Equipment object
    /// </summary>
    public class Equipment
    {
        /// <summary>
        /// Gets or sets Equipment object Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Equipment object Name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Equipment object Price.
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
    }
}
