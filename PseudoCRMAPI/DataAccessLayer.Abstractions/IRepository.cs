using Core.BaseEntities;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstractions
{
    public interface IRepository<T>
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        Task<T?> ReadAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> ReadByCondition(Expression<Func<T, bool>> predicate, int skip, int take);
        Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate, int skip, int take);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
