using Core.Abstractions.Database;

namespace Core.Database
{
    public class DatabaseColumnCollection : IDatabaseQueryable
    {
        public IEnumerable<DatabaseColumn> Columns { get; set; }
    }
}