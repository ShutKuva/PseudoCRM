using Core;
using Core.ChatEntities;
using Core.Dtos.Chat;

namespace BusinessLogicLayer.Abstractions.Chat.Facades
{
    public interface IMessageFacade
    {
        Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync(int userId);
        Task<IEnumerable<MessageDto>> AddMessageByUserIdAsync(int userId, string messageText);
        Task<IEnumerable<MessageDto>> AddMessageByUserAsync(User user, string messageText);
    }
}