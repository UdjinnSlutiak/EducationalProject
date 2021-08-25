// <copyright file="EquipmentController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API Equipment Controller.
    /// </summary>
    [ApiController]
    [Route("equipments")]
    public class EquipmentController : ControllerBase
    {
        /// <summary>
        /// Variable uses to have access to equipment logic.
        /// </summary>
        private IEquipmentLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentController"/> class.
        /// Receives IEquipmentLogic instance by dependency injection to work with equipment repository.
        /// </summary>
        /// <param name="logic">IEquipmentLogic instance received by dependency injection.</param>
        public EquipmentController(IEquipmentLogic logic)
        {
            this.logic = logic;
        }

        /// <summary>
        /// Equipment CRUD Get method.
        /// </summary>
        /// <param name="offset">Count of Equipments to skip.</param>
        /// <param name="count">Count of Equipments to take.</param>
        /// <returns>IEnumerable collection of Equipment inastances.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int offset = 0, int count = 10)
        {
            return this.Ok(await this.logic.GetEquipmentsAsync(offset, count));
        }

        /// <summary>
        /// Equipment overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Equipment instance.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return this.Ok(await this.logic.GetEquipmentByIdAsync(id));
        }

        /// <summary>
        /// Equipment CRUD Create method.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task Create(Equipment equipment)
        {
            await this.logic.CreateEquipmentAsync(equipment);
        }

        /// <summary>
        /// Equipment CRUD Update method.
        /// </summary>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut]
        public async Task Update(Equipment equipment)
        {
            await this.logic.UpdateEquipmentAsync(equipment);
        }

        /// <summary>
        /// Equipment CRUD Delete method.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.logic.DeleteEquipmentAsync(id);
        }
    }
}
