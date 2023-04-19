using Core.Email;
using Core.Email.Additional;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace BusinessLogicLayer.Email.Protocols
{
    public static class Imap
    {
        public static async Task<IReadOnlyList<MimeMessage>> GetMessages(EmailCredentials emailCredentials, SearchQuery searchQuery = null!)
        {
            if ((emailCredentials.ServerProtocols & ServerProtocols.Imap) != ServerProtocols.Imap)
            {
                throw new ArgumentException(nameof(emailCredentials));
            }

            IEnumerable<ServerInformation> imapServerInformation = emailCredentials.ServerInformations.Where(si => (si.ServerInformation.ServerProtocol & ServerProtocols.Imap) == ServerProtocols.Imap).Select(si => si.ServerInformation);

            foreach (ServerInformation si in imapServerInformation)
            {
                List<MimeMessage> result = new List<MimeMessage>();

                try
                {
                    using var client = new ImapClient();
                    client.ServerCertificateValidationCallback = (a, b, c, d) => true;
                    await client.ConnectAsync(si.Server, si.Port);

                    await client.AuthenticateAsync(emailCredentials.Login, emailCredentials.Password);

                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

                    var uids = await client.Inbox.SearchAsync(searchQuery ?? SearchQuery.All);

                    foreach (var uid in uids)
                    {
                        var message = await client.Inbox.GetMessageAsync(uid);

                        result.Add(message);
                    }

                    await client.DisconnectAsync(true);

                    return result;
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