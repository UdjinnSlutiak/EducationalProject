// <copyright file="IUserRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAsync(int offset, int count);
        public Task<T> GetAsync(int id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);
    }
}
