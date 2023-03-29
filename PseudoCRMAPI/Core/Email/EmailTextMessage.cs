namespace Core.Email
{
    public class EmailTextMessage
    {
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> From { get; set; }
        public string? Subject { get; set; }
        public string Text { get; set; }
    }
}