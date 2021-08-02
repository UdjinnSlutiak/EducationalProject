using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("equipment")]
    public class EquipmentController : Controller
    {
        private IEquipmentLogic logic;
        public EquipmentController(IEquipmentLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Equipment> Get()
        {
            return logic.Get();
        }

        [HttpGet("{id}")]
        public Equipment Get(int id)
        {
            return logic.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Equipment equipment)
        {
            logic.Create(equipment);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Equipment equipment)
        {
            logic.Update(id, equipment);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
