namespace Domain.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public bool isValid()
        {
            if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Position))
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

        public override bool Equals(object obj)
        {
            User user = obj as User;
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
