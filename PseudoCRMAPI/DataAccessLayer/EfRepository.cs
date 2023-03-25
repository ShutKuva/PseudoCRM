using Core.BaseEntities;
using DataAccessLayer.Abstractions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer
{
    public class EfRepository<T, U, C> : IRepository<T, U> where T : BaseEntity<U> where C : DbContext
    {
        private readonly C _context;

        public EfRepository(C context)
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

        public void Delete(U id)
        {
            T? entity = Read(id);
            if (entity == null)
            {
                throw new ArgumentException(nameof(id));
            }
            _context.Remove(entity);
        }

        public async Task DeleteAsync(U id)
        {
            T? entity = await ReadAsync(id);
            if (entity == null)
            {
                throw new ArgumentException(nameof(id));
            }
            _context.Remove(entity);
        }

        public T? Read(U id)
        {
            return _context.Set<T>().Find();
        }

        public Task<T?> ReadAsync(U id)
        {
            return _context.Set<T>().FindAsync(id).AsTask();
        }

        public IEnumerable<T> ReadByCondition(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return _context.Set<T>().Where(predicate).Skip(skip).Take(take);
        }

        public Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate).Skip(skip).Take(take).AsEnumerable());
        }

        public void Update(T entity)
        {
            T? oldEntity = Read(entity.Id);
            if (oldEntity == null)
            {
                Create(entity);
            }

            EntityEntry<T?> entry = _context.Entry(oldEntity);
            entry.CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            T? oldEntity = await ReadAsync(entity.Id);
            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }

            EntityEntry<T?> entry = _context.Entry(oldEntity);
            entry.CurrentValues.SetValues(entity);
        }
    }
}