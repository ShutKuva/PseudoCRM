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

            ServerInformation smtpServerInformation = emailCredentials.ServerInformations[ServerProtocols.Smtp];

            using var client = new SmtpClient();

            await client.ConnectAsync(smtpServerInformation.Server, smtpServerInformation.Port, smtpServerInformation.SecureSocketOptions);
            
            await client.AuthenticateAsync(emailCredentials.Login, emailCredentials.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}