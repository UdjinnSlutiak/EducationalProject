// <copyright file="IUserRepository.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAsync(int offset, int count);
        public Task<T> GetAsync<TKey>(TKey id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task DeleteRangeAsync(IEnumerable<T> entities);
        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);
    }
}
