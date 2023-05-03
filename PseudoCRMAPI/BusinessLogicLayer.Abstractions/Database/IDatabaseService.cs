using Core.Database;

namespace BusinessLogicLayer.Abstractions.Database
{
    public interface IDatabaseService<TDatabase, TCollection, TPredicate, TColumnCollection, TCollectionRelations, TResult> 
        where TPredicate : class
        where TColumnCollection : class
        where TCollectionRelations : class
    {
        Task CreateDatabase(string connectionString, string databaseName = null);
        Task<IEnumerable<DatabaseEntity>> GetEntities(
            TDatabase database,
            TCollection collection,
            TPredicate predicate = null,
            TColumnCollection columns = null,
            TColumnCollection groupBycolumns = null,
            TCollectionRelations relationCollection = null,
            int skip = 0,
            int take = 0);
    }
}
