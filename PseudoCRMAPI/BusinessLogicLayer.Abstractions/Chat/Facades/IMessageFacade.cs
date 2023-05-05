using Core;
using Core.ChatEntities;

namespace BusinessLogicLayer.Abstractions.Chat.Facades
{
    public interface IMessageFacade
    {
        Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId);
        Task<IEnumerable<Message>> AddMessageByUserIdAsync(int userId, string messageText);
        Task<IEnumerable<Message>> AddMessageByUserAsync(User user, string messageText);
    }
}