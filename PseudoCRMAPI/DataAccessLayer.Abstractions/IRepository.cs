using Core.BaseEntities;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstractions
{
    public interface IRepository<T, U> where T : BaseEntity<U>
    {
        void Create(T entity);
        Task CreateAsync(T entity);
        T? Read(U id);
        Task<T?> ReadAsync(U id);
        IEnumerable<T> ReadByCondition(Expression<Func<T, bool>> predicate, int skip, int take);
        Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate, int skip, int take);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(U id);
        Task DeleteAsync(U id);
    }
}
