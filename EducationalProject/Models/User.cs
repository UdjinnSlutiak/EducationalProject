namespace EducationalProject.Models
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
    }
}
