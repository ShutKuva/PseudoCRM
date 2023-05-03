using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Abstractions.Database;
using Core.Database;
using System.Text;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class SelectQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabaseColumnCollection _queryable;

        public SelectQueryGenerator(DatabaseColumnCollection queryable)
        {
            _queryable = queryable;
        }

        public string GetQuery()
        {
            StringBuilder result = new StringBuilder();

            if (_queryable.Columns != null)
            {
                result.Append($"select {string.Join(", ", _queryable.Columns)}");
            }
            else
            {
                result.Append("select *");
            }

            return result.ToString();
        }
    }
}