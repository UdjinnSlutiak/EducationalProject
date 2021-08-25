// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Models.Dto;
    using EquipmentControll.Domain.Models.Requests;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API User Controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Variable uses to have access to user logic.
        /// </summary>
        private readonly IUserLogic userLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// Receives IUserLogic instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="userLogic">IUserLogic instance received by dependency injection.</param>
        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
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
            IEnumerable<User> users = await this.userLogic.GetUsersAsync(offset, count);
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
            User user = await this.userLogic.GetUserByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            UserDto userDto = new (user);

            return this.Ok(userDto);
        }

        [HttpPost("{id}/change-role")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleRequest request)
        {
            await this.userLogic.ChangeUserRoleAsync(request.Id, request.Role);
            return this.Ok();
        }

        /// <summary>
        /// User CRUD Update method.
        /// </summary>
        /// <param name="user">User instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task Update(User user)
        {
            await this.userLogic.UpdateUserAsync(user);
        }

        /// <summary>
        /// User CRUD Delete method.
        /// </summary>
        /// <param name="id">User to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task Delete(int id)
        {
            await this.userLogic.DeleteUserAsync(id);
        }
    }
}
