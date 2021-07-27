using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EducationalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalProject.Controllers
{
    [Route("equipment")]
    public class EquipmentController : Controller
    {
        private readonly ProjectContext context;

        public EquipmentController (ProjectContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Equipment> Get()
        {
            return context.Equipments.ToList();
        }

        [HttpGet("{id}")]
        public Equipment Get(int id)
        {
            return context.Equipments.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] Equipment equipment)
        {
            if (equipment.isValid())
            {
                context.Add(equipment);
                context.SaveChangesAsync();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Equipment equipment)
        {
            if (equipment.isValid(id))
            {
                equipment.Id = id;
                context.Update(equipment);
                context.SaveChangesAsync();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Equipments.Find(id));
                context.SaveChangesAsync();
            }
        }
    }
}
