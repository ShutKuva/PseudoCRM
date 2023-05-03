using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class GroupByQueryGeneratorExtensions
    {
        public static IQuery<string> UseGroupByGenerator(this IQuery<string> query, DatabaseColumnCollection columnCollection)
        {
            query.QueryGenerators.Add(new GroupByQueryGenerator(columnCollection));
            return query;
        }
    }
}