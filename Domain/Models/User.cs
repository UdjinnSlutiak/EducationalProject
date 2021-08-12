using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 4)]
        public string Position { get; set; }

        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Position))
                return true;
            else
                return false;
        }

        public bool IsValid(int id)
        {
            if (IsValid() && id > 0)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is User user)
                return Equals(user);
            return false;
        }

        public bool Equals(User user)
        {
            if (this.Id == user.Id && this.Name == user.Name && this.Position == user.Position)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
