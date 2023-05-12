using Core.BaseEntities;
using Core.ChatEntities;

namespace Core
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = new Chat();
    }
}