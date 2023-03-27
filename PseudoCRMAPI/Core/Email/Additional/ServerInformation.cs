using MailKit.Security;

namespace Core.Email.Additional
{
    public class ServerInformation
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions SecureSocketOptions { get; set; }
    }
}