// <copyright file="UserController.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Models.BindingTargets;
    using EquipmentControll.Domain.Models.Dto;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API User Controller.
    /// </summary>
    [ApiController]
    [Route("users")]
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
        /// <returns>IEnumerable collection of UserDto inastances.</returns>
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return this.logic.Get().Select(u => new UserDto(u));
        }

        /// <summary>
        /// User overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>User instance.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = this.logic.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            UserDto userDto = new UserDto(user);

            return Ok(userDto);
        }

        /// <summary>
        /// User CRUD Create method.
        /// </summary>
        /// <param name="target">User instance to add to database.</param>
        [HttpPost]
        public IActionResult Create(UserBindingTarget target)
        {
            User user = target.ToUser();
            this.logic.Create(user);
            return Ok(user);
        }

        /// <summary>
        /// User CRUD Update method.
        /// </summary>
        /// <param name="user">User instance that contains information to update.</param>
        [HttpPut("{id}")]
        public void Update(User user)
        {
            this.logic.Update(user);
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
