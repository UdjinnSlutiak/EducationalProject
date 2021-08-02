using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Repositories;

namespace Logic
{
    public class RecordLogic : IRecordLogic
    {
        private IRecord repository;

        public RecordLogic(IRecord repository)
        {
            this.repository = repository;
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
