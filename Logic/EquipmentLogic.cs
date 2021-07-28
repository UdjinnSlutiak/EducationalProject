using System;
using System.Collections.Generic;
using Data.Models;
using Data.Repositories;

namespace Logic
{
    public class EquipmentLogic : IEquipment
    {
        private EquipmentRepository repository;

        public EquipmentLogic()
        {
            repository = new();
        }

        public IEnumerable<Equipment> Get()
        {
            return repository.Get();
        }

        public Equipment Get(int id)
        {
            return repository.Get(id);
        }

        public void Create(Equipment equipment)
        {
            repository.Create(equipment);
        }

        public void Update(int id, Equipment equipment)
        {
            repository.Update(id, equipment);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
