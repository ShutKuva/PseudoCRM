using Core.Email.Additional;
using Core;
using MimeKit;
using BusinessLogicLayer.Email.Protocols;
using BusinessLogicLayer.Email.Shared;
using Core.Email;
using DataAccessLayer.Abstractions;
using BusinessLogicLayer.Abstractions.Email;

namespace BusinessLogicLayer.Email.Services
{
    public class ImapAndPopMessageService : EmailShared, IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, ServerProtocols>
    {
        public ImapAndPopMessageService(IRepository<User> userRepository) : base(userRepository)
        {
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, ServerProtocols serverProtocol = ServerProtocols.Imap)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            switch (serverProtocol)
            {
                case ServerProtocols.Pop:
                    return Pop.GetMessages(emailCredentials);
                default:
                case ServerProtocols.Imap:
                    return Imap.GetMessages(emailCredentials);
            }
        }
    }
}