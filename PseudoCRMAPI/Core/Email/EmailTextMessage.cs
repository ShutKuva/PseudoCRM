namespace Core.Email
{
    public class EmailTextMessage
    {
        public string To { get; set; }
        public string? Subject { get; set; }
        public string Text { get; set; }
    }
}