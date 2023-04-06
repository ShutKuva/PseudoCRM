﻿using Core.BaseEntities;
using DataAccessLayer.Abstractions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CrmDbContext _context;

        public EfRepository(CrmDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public Task CreateAsync(T entity)
        {
            return _context.AddAsync(entity).AsTask();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
        }

        public Task<T?> ReadAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public IEnumerable<T> ReadByCondition(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return _context.Set<T>().Where(predicate).Skip(skip).Take(take);
        }

        public Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate).Skip(skip).Take(take).AsEnumerable());
        }

        public async Task UpdateAsync(T entity)
        {
            T? oldEntity = await ReadAsync(e => e.Id == entity.Id);
            if (oldEntity == null)
            {
                await CreateAsync(entity);
                return;
            }

            EntityEntry<T?> entry = _context.Entry(oldEntity);
            entry.CurrentValues.SetValues(entity);
        }
    }
}