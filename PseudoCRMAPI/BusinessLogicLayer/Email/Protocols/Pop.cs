using System.Reflection.Metadata.Ecma335;
using Core.Email;
using Core.Email.Additional;
using MailKit;
using MailKit.Net.Pop3;
using MailKit.Search;
using MimeKit;

namespace BusinessLogicLayer.Email.Protocols
{
    public static class Pop
    {
        public static async Task<IReadOnlyList<MimeMessage>> GetMessages(EmailCredentials emailCredentials, int takeLast = 0)
        {
            if ((emailCredentials.ServerProtocols & ServerProtocols.Pop) != ServerProtocols.Pop)
            {
                throw new ArgumentException(nameof(emailCredentials));
            }

            ServerInformation? popServerInformation = emailCredentials.ServerInformations.FirstOrDefault(si => (si.ServerInformation.ServerProtocol & ServerProtocols.Pop) == ServerProtocols.Pop)?.ServerInformation;

            if (popServerInformation == null)
            {
                throw new ArgumentException("There is no registered server information for this protocol.");
            }

            using Pop3Client client = new Pop3Client();

            await client.ConnectAsync(popServerInformation.Server, popServerInformation.Port, popServerInformation.SecureSocketOptions);

            await client.AuthenticateAsync(emailCredentials.Login, emailCredentials.Password);

            IList<MimeMessage> result =  await client.GetMessagesAsync(Enumerable.Range(client.Count - (takeLast == 0 ? client.Count : takeLast) < 0 ? 0 : client.Count - takeLast, client.Count).ToList());

            await client.DisconnectAsync(true);

            return result.AsReadOnly();
        }
    }
}