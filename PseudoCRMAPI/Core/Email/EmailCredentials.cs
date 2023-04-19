using Core.BaseEntities;
using Core.Email.Additional;

namespace Core.Email
{
    public class EmailCredentials : BaseEntity
    {
        public string? PublicName { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ServerProtocols ServerProtocols { get; set; }
        public List<EmailCredentialsServerInformation> ServerInformations { get; set; } = new List<EmailCredentialsServerInformation>();

        public int? UserId { get; set; }
    }
}