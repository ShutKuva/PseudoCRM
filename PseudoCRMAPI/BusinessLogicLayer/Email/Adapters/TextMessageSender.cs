using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using Core;
using Core.Email;

namespace BusinessLogicLayer.Email.Adapters
{
    public class TextMessageSender : IMessageSender<User, EmailTextMessage>
    {
        public TextMessageSender(IEmailService<User, MimeKit>)
        {
            
        }

        public void SendMessage(User user, EmailTextMessage message)
        {
            throw new NotImplementedException();
        }
    }
}