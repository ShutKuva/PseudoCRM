using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;
using Core.Pagination;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class SkipTakeQueryGeneratorExtensions
    {
        public static IQuery<string> UseSkipTakeQueryGenerator(this IQuery<string> query, PaginationConfiguration paginationConfiguration, DatabaseSkipTakePage queryable)
        {
            query.QueryGenerators.Add(new SkipTakeQueryGenerator(paginationConfiguration, queryable));
            return query;
        }
    }
}