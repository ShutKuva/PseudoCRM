using Core.Email;
using Core.Email.Additional;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLogicLayer.Email.Protocols
{
    public static class Smtp
    {
        public static async Task SendMessage(EmailCredentials emailCredentials, MimeMessage message)
        {
            if ((emailCredentials.ServerProtocols & ServerProtocols.Smtp) != ServerProtocols.Smtp)
            {
                throw new ArgumentException(nameof(emailCredentials));
            }

            IEnumerable<ServerInformation> smtpServerInformation = emailCredentials.ServerInformations
                .Where(si => (si.ServerInformation.ServerProtocol & ServerProtocols.Smtp) == ServerProtocols.Smtp)
                .Select(si => si.ServerInformation);

            foreach (ServerInformation si in smtpServerInformation)
            {
                try
                {
                    using var client = new SmtpClient();
                    client.ServerCertificateValidationCallback = (a, b, c, d) => true;

                    await client.ConnectAsync(si.Server, si.Port, si.SecureSocketOptions);

                    await client.AuthenticateAsync(emailCredentials.Login, emailCredentials.Password);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);

                    return;
                }
                catch
                {
                    continue;
                }
            }
            
            throw new ArgumentException("There is no registered server information for this protocol.");
        }
    }
}