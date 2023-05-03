using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class SelectQueryGeneratorExtensions
    {
        public static IQuery<string> UseSelectQueryGenerator(this IQuery<string> query, DatabaseColumnCollection columnCollection)
        {
            query.QueryGenerators.Add(new SelectQueryGenerator(columnCollection));
            return query;
        }
    }
}