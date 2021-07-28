namespace Data.Models
{
    public class Equipment
    {

        public int Id { get; set; }
        public string Name { get; set; }
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
