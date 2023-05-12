using Core.BaseEntities;

namespace Core.ChatEntities
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
    }
}