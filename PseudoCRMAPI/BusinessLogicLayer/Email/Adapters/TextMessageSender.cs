using BusinessLogicLayer.Abstractions.Email.Adapters;
using Core;
using Core.Email;

namespace BusinessLogicLayer.Email.Adapters
{
    public class TextMessageSender : IMessageSender<User, EmailTextMessage>
    {
    }
}