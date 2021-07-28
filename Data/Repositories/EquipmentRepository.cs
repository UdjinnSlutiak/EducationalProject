using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Data.Repositories;

namespace Data.Repositories
{
    public class EquipmentRepository : IEquipment
    {

        private readonly ProjectContext context;

        public EquipmentRepository ()
        {
            context = new();
        }

        public IEnumerable<Equipment> Get()
        {
            return context.Equipments.ToList();
        }

        public Equipment Get(int id)
        {
            return context.Equipments.Find(id);
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
                context.Remove(context.Equipments.Find(id));
                context.SaveChangesAsync();
            }
        }

    }
}
