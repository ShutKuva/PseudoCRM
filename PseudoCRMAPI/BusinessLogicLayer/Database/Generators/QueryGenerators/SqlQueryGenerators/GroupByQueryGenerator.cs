using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class GroupByQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabaseColumnCollection _queryable;

        public GroupByQueryGenerator(DatabaseColumnCollection queryable)
        {
            _queryable = queryable;
        }

        public string GetQuery()
        {
            return $"group by {string.Join(", ", _queryable.Columns)}";
        }
    }
}