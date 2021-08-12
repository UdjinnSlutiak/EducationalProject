using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {

        private readonly IProjectContext context;

        public EquipmentRepository(IProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<Equipment> Get()
        {
            return context.Equipments.ToList();
        }

        public Equipment Get(int id)
        {
            return context.Equipments.FirstOrDefault(e => e.Id == id);
        }

        public void Create(Equipment equipment)
        {
            if (equipment.isValid())
            {
                context.Add(equipment);
                context.SaveChangesAsync();
            }
        }

        public void Update(int id, Equipment equipment)
        {
            if (equipment.isValid(id))
            {
                equipment.Id = id;
                context.Update(equipment);
                context.SaveChangesAsync();
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Equipments.FirstOrDefault(e => e.Id == id));
                context.SaveChangesAsync();
            }
        }

    }
}
