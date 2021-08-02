using System.Collections.Generic;
using Data.Models;

namespace Logic
{
    public interface IEquipmentLogic
    {

        public IEnumerable<Equipment> Get();

        public Equipment Get(int id);

        public void Create(Equipment equipment);

        public void Update(int id, Equipment equipment);

        public void Delete(int id);

    }
}
