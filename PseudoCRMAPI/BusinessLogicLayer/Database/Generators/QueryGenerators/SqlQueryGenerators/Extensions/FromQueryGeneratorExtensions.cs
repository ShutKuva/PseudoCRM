using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class FromQueryGeneratorExtensions
    {
        public static IQuery<string> UseFromQueryGenerator(this IQuery<string> query, DatabaseCollection collection)
        {
            query.QueryGenerators.Add(new FromQueryGenerator(collection));

            return query;
        }
    }
}