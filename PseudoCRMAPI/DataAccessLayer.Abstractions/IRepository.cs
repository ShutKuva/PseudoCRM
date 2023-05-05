using Core.BaseEntities;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstractions
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task<T?> ReadAsync(Expression<Func<T, bool>> predicate, int skip, int page);
        Task<IEnumerable<T>> ReadCollectionAsync(Expression<Func<T, bool>> predicate, int skip, int take, int page);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
