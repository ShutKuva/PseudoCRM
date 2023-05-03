using Core.Abstractions.Database;
using Core.BaseEntities;

namespace Core.Database
{
    public class DatabaseCollection : BaseEntity, IDatabaseQueryable
    {
        public string Name { get; set; }
        public DatabaseObject Database { get; set; }
        public DatabaseColumn PrimaryColumn { get; set; }
        public List<DatabaseColumn> Columns { get; set; } = new List<DatabaseColumn>();
    }
}