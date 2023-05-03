using Core.Database.Enums;

namespace Core.Database.Dtos
{
    public class DatabasePredicateDto
    {
        public object? Data { get; set; }
        public DatabaseColumnDto? Column { get; set; }
        public DatabasePredicateDto? Left { get; set; }
        public DatabasePredicateDto? Right { get; set; }
        public DatabaseOperators? Operator { get; set; }
    }
}