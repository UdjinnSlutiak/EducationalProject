﻿// Copyright 
//
// Equipment Controller Project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EquipmentControll.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentControll.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly ProjectContext context;

        public Repository(ProjectContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> GetAsync(int offset, int count)
        {
            return await this.context.Set<T>().Skip(offset).Take(count).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await this.context.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this.context.Remove(await this.context.Set<T>().FindAsync(id));
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
