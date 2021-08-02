using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ProjectContext context;

        public UserRepository()
        {
            context = new();
        }

        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        public User Get(int id)
        {
            return context.Users.Find(id);
        }

        public void Create(User user)
        {
            if (user.isValid())
            {
                context.Add(user);
                context.SaveChangesAsync();
            }
        }

        public void Update(int id, User user)
        {
            if (user.isValid(id))
            {
                user.Id = id;
                context.Update(user);
                context.SaveChangesAsync();
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Users.Find(id));
                context.SaveChangesAsync();
            }
        }
    }
}
