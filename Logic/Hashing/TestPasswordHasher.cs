namespace EquipmentControll.Logic.Hashing
{
    public class TestPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return $"hashed_{password}";
        }

        public bool Verify(string password, string hashedPassword)
        {
            return $"hashed_{password}" == hashedPassword;
        }
    }
}
