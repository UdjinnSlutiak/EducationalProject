// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        /// Initializes a new instance of the <see cref="UserController"/> class.
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
        /// <param name="offset">Count of Users to skip.</param>
        /// <param name="count">Count of Users to take.</param>
        /// <returns>IEnumerable collection of UserDto inastances.</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get(int offset = 0, int count = 10)
        {
            IEnumerable<User> users = await this.logic.GetUsersAsync(offset, count);
            return users.Select(u => new UserDto(u));
        }

        /// <summary>
        /// User overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">User to find Id value.</param>
        /// <returns>User instance.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await this.logic.GetUserByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            UserDto userDto = new (user);

            return this.Ok(userDto);
        }

        /// <summary>
        /// User CRUD Create method.
        /// </summary>
        /// <param name="target">User instance to add to database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserBindingTarget target)
        {
            User user = target.ToUser();
            await this.logic.CreateUserAsync(user);
            return this.Ok(user);
        }

        /// <summary>
        /// User CRUD Update method.
        /// </summary>
        /// <param name="user">User instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("{id}")]
        public async Task Update(User user)
        {
            await this.logic.UpdateUserAsync(user);
        }

        /// <summary>
        /// User CRUD Delete method.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.logic.DeleteUserAsync(id);
        }
    }
}
