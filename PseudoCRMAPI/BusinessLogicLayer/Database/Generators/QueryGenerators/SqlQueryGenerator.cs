using System.Text;
using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators
{
    public class SqlQueryGenerator : IQueryGenerator
    {
        public string GetQuery(DatabaseCollection collection = null, DatabasePredicate predicate = null, IEnumerable<DatabaseColumn> columns = null, int skip = 0, int take = 0, int page = 0)
        {
            StringBuilder result = new StringBuilder();

            if (columns != null)
            {
                result.Append($"select {string.Join(", ", columns)} ");
            }
            else
            {
                result.Append($"select * ");
            }

            if (collection != null)
            {
                result.Append($"from {collection.Name} ");
            }
            else
            {
                throw new ArgumentException("It is necessary to specify collection.");
            }

            if (predicate != null)
            {
                List<DatabaseCollection> collections = predicate.GetAllJoinedCollections();
                result.Append($"from {string.Join(", ", collections.Select(c => c.Name))} ");
            }

            if (predicate != null)
            {

            }

            return result.ToString();
        }
    }
}