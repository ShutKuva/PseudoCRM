using Core.Abstractions.Database;

namespace Core.Database
{
    public class DatabasePredicateWithPrimeCollection : IDatabaseQueryable
    {
        public DatabaseCollection PrimeCollection { get; set; }
        public DatabasePredicate Predicate { get; set; }
    }
}