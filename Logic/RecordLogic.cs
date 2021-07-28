using System;
using System.Collections.Generic;
using Data.Models;
using Data.Repositories;

namespace Logic
{
    public class RecordLogic : IRecord
    {
        private RecordRepository repository;

        public RecordLogic()
        {
            this.repository = new();
        }

        public IEnumerable<string> Get()
        {
            return repository.Get();
        }

        public string Get(int id)
        {
            return repository.Get(id);
        }

        public void Create(Record record)
        {
            repository.Create(record);
        }

        public void Update(int id, Record record)
        {
            repository.Update(id, record);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
