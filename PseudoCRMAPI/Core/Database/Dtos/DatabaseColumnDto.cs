using Core.Database.Enums;

namespace Core.Database.Dtos
{
    public class DatabaseColumnDto
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string Name { get; set; }
        public string Function { get; set; }
    }
}