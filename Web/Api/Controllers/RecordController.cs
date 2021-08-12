using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Controllers
{
    [Route("record")]
    public class RecordController : Controller
    {
        private IRecordLogic logic;
        public RecordController(IRecordLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return logic.Get();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return logic.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Record partialRecord)
        {
            if (ModelState.IsValid)
                logic.Create(partialRecord);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Record partialRecord)
        {
            if (ModelState.IsValid)
                logic.Update(id, partialRecord);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
