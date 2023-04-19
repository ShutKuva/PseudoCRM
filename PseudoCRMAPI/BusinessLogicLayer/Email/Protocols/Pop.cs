using Core.Email;
using Core.Email.Additional;
using MailKit.Net.Pop3;
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

            IEnumerable<ServerInformation> popServerInformation = emailCredentials.ServerInformations
                .Where(si => (si.ServerInformation.ServerProtocol & ServerProtocols.Pop) == ServerProtocols.Pop)
                .Select(si => si.ServerInformation);

            foreach (ServerInformation si in popServerInformation)
            {
                try
                {
                    using Pop3Client client = new Pop3Client();
                    client.ServerCertificateValidationCallback = (a, b, c, d) => true;
                    await client.ConnectAsync(si.Server, si.Port, si.SecureSocketOptions);

                    await client.AuthenticateAsync(emailCredentials.Login, emailCredentials.Password);

                    IList<MimeMessage> result = await client.GetMessagesAsync(Enumerable
                        .Range(
                            client.Count - (takeLast == 0 ? client.Count : takeLast) < 0 ? 0 : client.Count - takeLast,
                            client.Count).ToList());

                    await client.DisconnectAsync(true);

                    return result.AsReadOnly();
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