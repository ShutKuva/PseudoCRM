using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class PredicateQueryGeneratorExtensions
    {
        public static IQuery<string> UsePredicateQueryGenerator(this IQuery<string> query, DatabasePredicate predicateWithPrimeCollection)
        {
            query.QueryGenerators.Add(new PredicateQueryGenerator(predicateWithPrimeCollection));
            return query;
        }
    }
}