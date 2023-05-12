using Core.BaseEntities;
using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CrmDbContext _context;

        public EfRepository(CrmDbContext context)
        {
            _context = context;
        }

        public virtual Task CreateAsync(T entity)
        {
            return _context.AddAsync(entity).AsTask();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
        }

        public virtual Task<T?> ReadAsync(Expression<Func<T, bool>> predicate, int skip, int page)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual Task<IEnumerable<T>> ReadCollectionAsync(Expression<Func<T, bool>> predicate, int skip, int take, int page)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate).Skip(skip).Take(take).AsEnumerable());
        }

        public virtual async Task UpdateAsync(T entity)
        {
            T? oldEntity = await ReadAsync(e => e.Id == entity.Id, 0, 0);
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