using Core.BaseEntities;
using Core.Database.Enums;

namespace Core.Database
{
    public class DatabaseColumn : BaseEntity
    {
        public string Name { get; set; }
        public DatabaseTypes Type { get; set; }
        public int ColumnId { get; set; }
        public string Function { get; set; }

        public int? CollectionId { get; set; }
        public DatabaseCollection? Collection { get; set; }

        public int? ForeignCollectionId { get; set; }
        public DatabaseCollection? ForeignCollection { get; set; }

        public override string ToString()
        {
            return $"[{Collection.Name}].[{Name}]";
        }
    }
}