using Core.Database;

namespace BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators
{
    public interface IQueryGenerator
    {
        public string GetQuery(DatabaseCollection collection = null, DatabasePredicate predicate = null, IEnumerable<DatabaseColumn> columns = null, int skip = 0, int take = 0, int page = 0);
    }
}
