﻿using Cet37Market.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        public DataContext context;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);

            await SaveAllAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);

            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);

            await SaveAllAsync();

            return entity;
        }

        private async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
