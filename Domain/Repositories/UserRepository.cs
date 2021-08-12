using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IProjectContext context;

        public UserRepository(IProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        public User Get(int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Create(User user)
        {
            if (user.IsValid())
            {
                context.Add(user);
                context.SaveChangesAsync();
            }
        }

        public void Update(int id, User user)
        {
            if (user.IsValid(id))
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
                context.Remove<User>(context.Users.FirstOrDefault(u => u.Id == id));
                context.SaveChangesAsync();
            }
        }
    }
}
