// <copyright file="UserRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Realization of IUserRepository interface. Part of repository pattern.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Variable uses to have access to database.
        /// </summary>
        private readonly IProjectContext context;

        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// Receives IProjectContext instance by dependency injection to work with database.
        /// </summary>
        /// <param name="context">IProjectContext instance received by dependency injection.</param>
        public UserRepository(IProjectContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Realization of IUserRepository Get method.
        /// </summary>
        /// <returns>IEnumerable collection of User inastances.</returns>
        public IEnumerable<User> Get()
        {
            return this.context.Users.ToList();
        }

        /// <summary>
        /// Realization of IUserRepository overloaded Get method.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>User instance.</returns>
        public User Get(int id)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Realization of IUserRepository Create method.
        /// </summary>
        /// <param name="user">User instance to add to database.</param>
        public void Create(User user)
        {
            this.context.Add(user);
            this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Realization of IUserRepository Update method.
        /// </summary>
        /// <param name="id">User to update Id value.</param>
        /// <param name="user">User instance that contains information to update.</param>
        public void Update(int id, User user)
        {
            user.Id = id;
            this.context.Update(user);
            this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Realization of IUserRepository Delete method.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        public void Delete(int id)
        {
            if (id > 0)
            {
                this.context.Remove<User>(this.context.Users.FirstOrDefault(u => u.Id == id));
                this.context.SaveChangesAsync();
            }
        }
    }
}
