using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IEquipmentRepository
    {
        public IEnumerable<Equipment> Get();

        public Equipment Get(int id);

        public void Create(Equipment equipment);

        public void Update(int id, Equipment equipment);

        public void Delete(int id);
    }
}
