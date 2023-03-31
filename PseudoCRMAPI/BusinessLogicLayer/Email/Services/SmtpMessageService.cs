using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Email.Protocols;
using BusinessLogicLayer.Email.Shared;
using Core;
using Core.Email;
using DataAccessLayer.Abstractions;
using MimeKit;

namespace BusinessLogicLayer.Email.Services
{
    public class SmtpMessageService : EmailShared, IMessageSender<User, string, MimeMessage>
    {
        public SmtpMessageService(IRepository<User> userRepository) : base(userRepository)
        {
        }

        public Task SendMessage(User user, string publicName, MimeMessage message)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Smtp.SendMessage(emailCredentials, message);
        }
    }
}