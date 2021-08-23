// <copyright file="UserController.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API User Controller.
    /// </summary>
    [Route("user")]
    public class UserController : Controller
    {
        /// <summary>
        /// Variable uses to have access to user logic.
        /// </summary>
        private IUserLogic logic;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// Receives IUserLogic instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="logic">IUserLogic instance received by dependency injection.</param>
        public UserController(IUserLogic logic)
        {
            this.logic = logic;
        }

        /// <summary>
        /// User CRUD Get method.
        /// </summary>
        /// <returns>IEnumerable collection of User inastances.</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.logic.Get();
        }

        /// <summary>
        /// User overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>User instance.</returns>
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return this.logic.Get(id);
        }

        /// <summary>
        /// User CRUD Create method.
        /// </summary>
        /// <param name="user">User instance to add to database.</param>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                this.logic.Create(user);
            }
        }

        /// <summary>
        /// User CRUD Update method.
        /// </summary>
        /// <param name="id">User to update Id value.</param>
        /// <param name="user">User instance that contains information to update.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                this.logic.Update(id, user);
            }
        }

        /// <summary>
        /// User CRUD Delete method.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
