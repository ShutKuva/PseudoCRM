using Core.Abstractions.Database;
using Core.Database.Enums;

namespace Core.Database
{
    public class DatabasePredicate : IDatabaseQueryable
    {
        public object? Data { get; set; }
        public DatabaseColumn? Column { get; set; }
        public DatabasePredicate? Left { get; set; }
        public DatabasePredicate? Right { get; set; }
        public DatabaseOperators? Operator { get; set; }

        public List<DatabaseCollection> GetAllJoinedCollections(List<DatabaseCollection> collections = null)
        {
            collections ??= new List<DatabaseCollection>();
            if (Column != null)
            {
                if (Column.Collection != null && !collections.Contains(Column.Collection))
                {
                    collections.Add(Column.Collection);
                }
            }

            Left?.GetAllJoinedCollections(collections);
            Right?.GetAllJoinedCollections(collections);

            return collections;
        }
    }
}