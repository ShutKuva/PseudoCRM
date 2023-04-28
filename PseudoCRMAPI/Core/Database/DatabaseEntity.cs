namespace Core.Database
{
    public class DatabaseEntity
    {
        public DatabaseCollection Collection { get; set; }
        public List<DatabaseKeyValue> KeyValue { get; set; }
    }
}