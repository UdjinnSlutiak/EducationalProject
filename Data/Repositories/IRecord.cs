using System.Collections.Generic;
using Data.Models;

namespace Data.Repositories
{
    public interface IRecord
    {
        public IEnumerable<string> Get();

        public string Get(int id);

        public void Create(Record record);

        public void Update(int id, Record record);

        public void Delete(int id);
    }
}
