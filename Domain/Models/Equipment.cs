using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Equipment
    {

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, 1000000)]
        public double Value { get; set; }

        public bool isValid()
        {
            if (!string.IsNullOrEmpty(this.Name) && this.Value > 0)
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
