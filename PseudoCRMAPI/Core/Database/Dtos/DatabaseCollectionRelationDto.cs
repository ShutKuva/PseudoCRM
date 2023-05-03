namespace Core.Database.Dtos
{
    public class DatabaseCollectionRelationDto
    {
        public DatabaseCollectionDto FirstCollection { get; set; }
        public DatabaseCollectionDto SecondCollection { get; set; }
    }
}