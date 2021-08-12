using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IRecordRepository
    {
        public IEnumerable<string> Get();

        public string Get(int id);

        public void Create(Record record);

        public void Update(int id, Record record);

        public void Delete(int id);
    }
}
