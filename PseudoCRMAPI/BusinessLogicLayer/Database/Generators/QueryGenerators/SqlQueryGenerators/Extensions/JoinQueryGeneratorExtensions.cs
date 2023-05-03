using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators.Extensions
{
    public static class JoinQueryGeneratorExtensions
    {
        public static IQuery<string> UseJoinQueryGenerator(this IQuery<string> query, DatabaseCollectionRelationCollection collectionRelationCollection)
        {
            query.QueryGenerators.Add(new JoinQueryGenerator(collectionRelationCollection));
            return query;
        }
    }
}