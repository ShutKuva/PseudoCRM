using AutoMapper;
using BusinessLogicLayer.Email.Shared;
using Core.Email.Additional;
using Core;
using DataAccessLayer.Abstractions;
using MimeKit;
using MailKit.Search;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;

namespace BusinessLogicLayer.Email.Adapters
{
    public class MessageReceiverAdapter<T, S> : 
        EmailShared,
        IStringMessageReceiverAdapter<T, S>
    {
        private readonly IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, S> _messageReceiver;
        private readonly IMapper _mapper;

        public MessageReceiverAdapter(
            IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, S> messageReceiver,
            IRepository<User> userRepository,
            IMapper mapper
            ) : base(userRepository)
        {
            _messageReceiver = messageReceiver;
            _mapper = mapper;
        }

        public async Task<T> GetMessages(string user, string mailQuery, S messageSearchQuery)
        {
            User userObj = await GetUserById(user);

            return _mapper.Map<T>(await _messageReceiver.GetMessages(userObj, mailQuery, messageSearchQuery));
        }
    }
}