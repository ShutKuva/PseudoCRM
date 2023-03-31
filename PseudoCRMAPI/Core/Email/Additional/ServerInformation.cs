using Core.BaseEntities;
using MailKit.Security;

namespace Core.Email.Additional
{
    public class ServerInformation : BaseEntity
    {
        public ServerProtocols ServerProtocol { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions SecureSocketOptions { get; set; }
    }
}