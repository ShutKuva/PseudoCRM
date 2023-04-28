using Core.BaseEntities;
using Core.Database.Connections.BaseEntities;
using Core.Database.Enums;

namespace Core.Database
{
    public class DatabaseObject : BaseEntity
    {
        public string Name { get; set; }
        public DatabaseProviders Provider { get; set; } 
        public DatabaseConnection Connection { get; set; } 
        public List<DatabaseCollection> Collections { get; set; }
    }
}