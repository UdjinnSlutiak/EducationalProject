// <copyright file="UserLogic.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Logic
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;

    /// <summary>
    /// Realization of IUserLogic interface. Part of repository pattern.
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// Variable uses to have access to user repository.
        /// </summary>
        private IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the UserLogic class.
        /// Receives IUserRepository instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="repository">IUserRepository instance received by dependency injection.</param>
        public UserLogic(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Realization of IUserLogic Get method.
        /// </summary>
        /// <returns>IEnumerable collection of User inastances.</returns>
        public IEnumerable<User> Get()
        {
            return this.repository.Get();
        }

        /// <summary>
        /// Realization of IUserLogic overloaded Get method.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>User instance.</returns>
        public User Get(int id)
        {
            return this.repository.Get(id);
        }

        /// <summary>
        /// Realization of IUserLogic Create method.
        /// </summary>
        /// <param name="user">User instance to add to database.</param>
        public void Create(User user)
        {
            this.repository.Create(user);
        }

        /// <summary>
        /// Realization of IUserLogic Update method.
        /// </summary>
        /// <param name="id">User to update Id value.</param>
        /// <param name="user">User instance that contains information to update.</param>
        public void Update(int id, User user)
        {
            this.repository.Update(id, user);
        }

        /// <summary>
        /// Realization of IUserLogic Delete method.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }
    }
}
