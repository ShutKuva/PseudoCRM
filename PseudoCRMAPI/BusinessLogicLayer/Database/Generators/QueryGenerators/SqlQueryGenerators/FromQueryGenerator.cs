using System.Text;
using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Abstractions.Database;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class FromQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabaseCollection _queryable;

        public FromQueryGenerator(DatabaseCollection queryable)
        {
            _queryable = queryable;
        }

        public string GetQuery()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"from {_queryable.Name}");

            return result.ToString();
        }
    }
}