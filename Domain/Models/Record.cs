using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Record
    {

        public int Id { get; set; }

        [Required]
        public User Sender { get; set; }

        [Required]
        public User Receiver { get; set; }

        [Required]
        public Equipment Equipment { get; set; }

        [NotMapped]
        public int senderId { get; set; }
        [NotMapped]
        public int receiverId { get; set; }
        [NotMapped]
        public int equipmentId { get; set; }

        public override string ToString()
        {
            return $"{this.Sender.Name} gave {this.Receiver.Name} {this.Equipment.Name}";
        }

        public bool isValid()
        {
            if (this.Sender != null && this.Receiver != null && this.Equipment != null)
                return true;
            else
                return false;
        }

        public bool isValid(int id)
        {
            if (isValid() && id > 0)
                return true;
            else
                return false;
        }
    }
}
