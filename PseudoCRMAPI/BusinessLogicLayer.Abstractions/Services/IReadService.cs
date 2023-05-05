using System.Linq.Expressions;

namespace BusinessLogicLayer.Abstractions.Services
{
    public interface IReadService<TEntity>
    {
        Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> predicate, int skip, int page);
        Task<IEnumerable<TEntity>> ReadCollectionAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, int page);
    }
}