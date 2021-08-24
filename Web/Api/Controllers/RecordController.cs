// <copyright file="RecordController.cs" company="PlaceholderCompany">
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
        /// Initializes a new instance of the <see cref="RecordController"/> class.
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
        /// <param name="offset">Count of Records to skip.</param>
        /// <param name="count">Count of Records to take.</param>
        /// <returns>IEnumerable collection of Record inastances.</returns>
        [HttpGet]
        public async Task<IEnumerable<Record>> Get(int offset = 0, int count = 10)
        {
            return await this.logic.GetRecordsAsync(offset, count);
        }

        /// <summary>
        /// Record overloaded CRUD Get method.
        /// </summary>
        /// <param name="id">Record to find Id value.</param>
        /// <returns>Record instance.</returns>
        [HttpGet("{id}")]
        public async Task<Record> Get(int id)
        {
            return await this.logic.GetRecordByIdAsync(id);
        }

        /// <summary>
        /// Record CRUD Create method.
        /// </summary>
        /// <param name="partialRecord">Record instance to add to database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task Create(Record partialRecord)
        {
            await this.logic.CreateRecordAsync(partialRecord);
        }

        /// <summary>
        /// Record CRUD Update method.
        /// </summary>
        /// <param name="partialRecord">Record instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("{id}")]
        public async Task Update(Record partialRecord)
        {
            await this.logic.UpdateRecordAsync(partialRecord);
        }

        /// <summary>
        /// Record CRUD Delete method.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.logic.DeleteRecordAsync(id);
        }
    }
}
