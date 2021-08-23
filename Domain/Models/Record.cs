// <copyright file="Record.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Model class that describes Record object
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Gets or sets Record object Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Record object Sender.
        /// </summary>
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        /// <summary>
        /// Gets or sets Record object SenderId.
        /// Uses only to receive SenderId from JSON and then find and add Sender to Record instance by this Id.
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Gets or sets Record object Receiver.
        /// </summary>
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }

        /// <summary>
        /// Gets or sets Record object ReceiverId.
        /// Uses only to receive ReceiverId from JSON and then find and add Receiver to Record instance by this Id.
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets Record object Equipment.
        /// </summary>
        public Equipment Equipment { get; set; }

        /// <summary>
        /// Gets or sets Record object EquipmentId.
        /// Uses only to receive EquipmentID from JSON and then find and add Equipment to Record instance by this Id.
        /// </summary>
        [Required]
        public int EquipmentId { get; set; }

        [Required]
        public DateTime GivenDate { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public bool IsReturned { get; set; }

        /// <summary>
        /// Overrided base ToString method.
        /// </summary>
        /// <returns>Record string that includes sender, receiver and given equipment names.</returns>
        public override string ToString()
        {
            return $"{this.Sender.Username} gave {this.Receiver.Username} {this.Equipment.Name}";
        }
    }
}
