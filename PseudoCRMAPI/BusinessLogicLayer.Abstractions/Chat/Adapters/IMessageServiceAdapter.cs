using Core.ChatEntities;

namespace BusinessLogicLayer.Abstractions.Chat.Adapters
{
    public interface IMessageServiceAdapter<TMessage>
    {
        Task<IEnumerable<Message>> GetMessagesAsync(string organizationName);
    }
}