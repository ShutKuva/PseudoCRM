using BusinessLogicLayer.Email.Protocols;
using Core.Email;
using Core;
using MimeKit;
using BusinessLogicLayer.Email.Shared;
using DataAccessLayer.Abstractions;
using BusinessLogicLayer.Abstractions.Email;

namespace BusinessLogicLayer.Email.Services
{
    public class PopMessageService : EmailShared, IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, int>
    {
        public PopMessageService(IRepository<User> userRepository) : base(userRepository)
        {
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, int takeLast)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Pop.GetMessages(emailCredentials, takeLast);
        }
    }
}