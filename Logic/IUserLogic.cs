using System.Collections.Generic;
using Domain.Models;

namespace Logic
{
    public interface IUserLogic
    {

        public IEnumerable<User> Get();

        public User Get(int id);

        public void Create(User user);

        public void Update(int id, User user);

        public void Delete(int id);

    }
}
