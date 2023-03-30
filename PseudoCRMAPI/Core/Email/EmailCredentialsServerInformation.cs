using Core.Email.Additional;

namespace Core.Email
{
    public class EmailCredentialsServerInformation
    {
        public int EmailCredentialsId { get; set; }
        public EmailCredentials EmailCredentials { get; set; }

        public int ServerInformationId { get; set; }
        public ServerInformation ServerInformation { get; set; }
    }
}