using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("record")]
    public class RecordController : Controller
    {
        private RecordLogic logic;
        public RecordController ()
        {
            logic = new();
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
            logic.Create(partialRecord);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Record partialRecord)
        {
            logic.Update(id, partialRecord);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
