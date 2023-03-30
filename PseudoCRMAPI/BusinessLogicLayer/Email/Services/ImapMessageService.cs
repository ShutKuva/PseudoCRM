using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Email.Protocols;
using BusinessLogicLayer.Email.Shared;
using Core;
using Core.Email;
using DataAccessLayer.Abstractions;
using MailKit.Search;
using MimeKit;

namespace BusinessLogicLayer.Email.Services
{
    public class ImapMessageService : EmailShared, IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, SearchQuery>
    {
        public ImapMessageService(IRepository<User> userRepository) : base(userRepository)
        {
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, SearchQuery searchQuery)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Imap.GetMessages(emailCredentials, searchQuery);
        }
    }
}