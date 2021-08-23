// <copyright file="RecordController.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Web.Api.Controllers
{
    using System.Collections.Generic;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CRUD API Record Controller.
    /// </summary>
    [ApiController]
    [Route("records")]
    public class RecordController : Controller
    {
        /// <summary>
        /// Variable uses to have access to record logic.
        /// </summary>
        private IRecordLogic logic;

        /// <summary>
        /// Initializes a new instance of the RecordController class.
        /// Receives IRecordLogic instance by dependency injection to work with user repository.
        /// </summary>
        /// <param name="logic">IRecordLogic instance received by dependency injection.</param>
        public RecordController(IRecordLogic logic)
        {
            this.logic = logic;
        }

        /// <summary>
        /// Record CRUD Get method.
        /// </summary>
        /// <returns>IEnumerable collection of Record inastances.</returns>
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return this.logic.Get();
        }

        /// <summary>
        /// Record overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">Record to find Id value.</param>
        /// <returns>Record instance.</returns>
        [HttpGet("{id}")]
        public Record Get(int id)
        {
            return this.logic.Get(id);
        }

        /// <summary>
        /// Record CRUD Create method.
        /// </summary>
        /// <param name="partialRecord">Record instance to add to database.</param>
        [HttpPost]
        public void Post(Record partialRecord)
        {
            this.logic.Create(partialRecord);
        }

        /// <summary>
        /// Record CRUD Update method.
        /// </summary>
        /// <param name="id">Record to update Id value.</param>
        /// <param name="partialRecord">Record instance that contains information to update.</param>
        [HttpPut("{id}")]
        public void Put(Record partialRecord)
        {
            this.logic.Update(partialRecord);
        }

        /// <summary>
        /// Record CRUD Delete method.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
