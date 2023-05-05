using Core.BaseEntities;

namespace Core.ChatEntities
{
    public class Chat : BaseEntity
    {
        public List<Message> Messages { get; set; }
        public Organization Organization { get; set; }
    }
}