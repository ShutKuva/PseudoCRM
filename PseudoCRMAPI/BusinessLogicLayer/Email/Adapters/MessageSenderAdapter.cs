using AutoMapper;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using BusinessLogicLayer.Email.Shared;
using Core;
using Core.Email;
using DataAccessLayer.Abstractions;
using MimeKit;

namespace BusinessLogicLayer.Email.Adapters
{
    public class MessageSenderAdapter<T> : EmailShared, IStringMessageSenderAdapter<T>
    {
        private readonly IMessageSender<User, string, MimeMessage> _mailSender;
        private readonly IMapper _mapper;

        public MessageSenderAdapter(
            IMessageSender<User, string, MimeMessage> mailSender,
            IRepository<User> userRepository,
            IMapper mapper) : base(userRepository)
        {
            _mailSender = mailSender;
            _mapper = mapper;
        }

        public async Task SendMessage(string user, string mailQuery, T message)
        {
            await _mailSender.SendMessage(await GetUserById(user), mailQuery, _mapper.Map<MimeMessage>(message));
        }
    }
}