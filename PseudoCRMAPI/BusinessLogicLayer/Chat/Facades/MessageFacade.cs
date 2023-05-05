using BusinessLogicLayer.Abstractions.Chat;
using BusinessLogicLayer.Abstractions.Chat.Facades;
using Core;
using Core.ChatEntities;

namespace BusinessLogicLayer.Chat.Facades
{
    public class MessageFacade : IMessageFacade
    {
        private readonly IUserService<User> _userService;
        private readonly IMessageService<Message> _messageService;

        public MessageFacade(IUserService<User> userService, IMessageService<Message> messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
        {
            User? user = await _userService.ReadAsync(user => user.Id == userId, 0, 0);

            ValidateUser(user);

            return user.Organization.Chat.Messages;
        }

        public async Task<IEnumerable<Message>> AddMessageByUserIdAsync(int userId, string messageText)
        {
            User? user = await _userService.ReadAsync(user => user.Id == userId, 0, 0);

            return await AddMessageByUserAsync(user, messageText);
        }

        public async Task<IEnumerable<Message>> AddMessageByUserAsync(User user, string messageText)
        {
            ValidateUser(user);

            await _messageService.CreateAsync(new Message() { Sender = user, Text = messageText, ChatId = user.Organization.ChatId });

            return user.Organization.Chat.Messages;
        }

        private void ValidateUser(User? user)
        {
            if (user == null)
            {
                throw new ArgumentException("There is no user with this id.");
            }

            Organization? userOrganization = user.Organization;

            if (userOrganization == null)
            {
                throw new ArgumentException("User is no in organization.");
            }
        }
    }
}