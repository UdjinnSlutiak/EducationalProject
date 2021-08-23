// <copyright file="EquipmentController.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API Equipment Controller.
    /// </summary>
    [ApiController]
    [Route("equipments")]
    public class EquipmentController : Controller
    {
        /// <summary>
        /// Variable uses to have access to equipment logic.
        /// </summary>
        private IEquipmentLogic logic;

        /// <summary>
        /// Initializes a new instance of the EquipmentController class.
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
        /// <returns>IEnumerable collection of Equipment inastances.</returns>
        [HttpGet]
        public IEnumerable<Equipment> Get()
        {
            return this.logic.Get();
        }

        /// <summary>
        /// Equipment overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">Equipment to find Id value.</param>
        /// <returns>Equipment instance.</returns>
        [HttpGet("{id}")]
        public Equipment Get(int id)
        {
            return this.logic.Get(id);
        }

        /// <summary>
        /// Equipment CRUD Create method.
        /// </summary>
        /// <param name="equipment">Equipment instance to add to database.</param>
        [HttpPost]
        public void Post(Equipment equipment)
        {
            this.logic.Create(equipment);
        }

        /// <summary>
        /// Equipment CRUD Update method.
        /// </summary>
        /// <param name="id">Equipment to update Id value.</param>
        /// <param name="equipment">Equipment instance that contains information to update.</param>
        [HttpPut("{id}")]
        public void Put(Equipment equipment)
        {
            this.logic.Update(equipment);
        }

        /// <summary>
        /// Equipment CRUD Delete method.
        /// </summary>
        /// <param name="id">Equipment to delete Id value.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
