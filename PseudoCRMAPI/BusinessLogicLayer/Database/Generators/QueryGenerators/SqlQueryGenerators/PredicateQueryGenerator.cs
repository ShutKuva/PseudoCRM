using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Abstractions.Database;
using Core.Database;
using Core.Database.Enums;
using System.Text;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class PredicateQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabasePredicate _queryable;

        public PredicateQueryGenerator(DatabasePredicate queryable)
        {
            _queryable = queryable;
        }

        public string GetQuery()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"where {ProcessPredicate(_queryable)}");

            return result.ToString();
        }

        private string ProcessPredicate(DatabasePredicate predicate)
        {
            if (predicate == null)
            {
                return "";
            }

            StringBuilder result = new StringBuilder();

            if (predicate.Left != null)
            {
                result.Append(ProcessPredicate(predicate.Left));
            }

            if (predicate.Operator != null)
            {
                result.Append(GetStringRepresentationForOperator(predicate.Operator.Value) + " ");
            }

            if (predicate.Right != null)
            {
                result.Append(ProcessPredicate(predicate.Right));
            }

            if (predicate.Data != null)
            {
                result.Append(predicate.Data + " ");
            }

            if (predicate.Column != null)
            {
                result.Append(predicate.Column + " ");
            }

            return result.ToString();
        }

        private string GetStringRepresentationForOperator(DatabaseOperators dbOperator) => dbOperator switch
        {
            DatabaseOperators.And => "and",
            DatabaseOperators.Equals => "=",
            DatabaseOperators.LessThan => "<",
            DatabaseOperators.LessThanOrEqual => "<=",
            DatabaseOperators.MoreThan => ">",
            DatabaseOperators.MoreThanOrEqual => ">=",
            DatabaseOperators.NotEquals => "<>",
            DatabaseOperators.Or => "or",
        };
    }
}