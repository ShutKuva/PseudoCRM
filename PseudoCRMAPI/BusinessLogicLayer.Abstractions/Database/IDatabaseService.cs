namespace BusinessLogicLayer.Abstractions.Database
{
    public interface IDatabaseService
    {
        Task CreateDatabase(string connectionString, string databaseName = null);
        //Task<IEnumerable<DatabaseCollection>> GetCollections();
        //public Task<IEnumerable<T>> GetEntities<T>(DatabasePredicate predicate = null,
        //    string collectionName = "",
        //    int skip = 0,
        //    int take = 0,
        //    List<DatabaseColumn> columns = null);
        //Task AddEntity<T>(T entity, string collectionName = "");
    }
}
