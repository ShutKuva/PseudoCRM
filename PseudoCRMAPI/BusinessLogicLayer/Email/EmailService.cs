using AutoMapper;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using BusinessLogicLayer.Email.Protocols;
using Core;
using Core.Dtos;
using Core.Email;
using Core.Email.Additional;
using DataAccessLayer.Abstractions;
using MailKit.Search;
using MimeKit;

namespace BusinessLogicLayer.Email
{
    public class EmailService : IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, ServerProtocols>, 
        IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, int>, 
        IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, SearchQuery>,
        IMessageSender<User, string, MimeMessage>,
        IEmailService<User, EmailCredentials>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<EmailCredentials> _emailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmailService(IRepository<User> userRepository, IRepository<EmailCredentials> emailRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, ServerProtocols serverProtocol = ServerProtocols.Imap)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            switch (serverProtocol)
            {
                case ServerProtocols.Pop:
                    return Pop.GetMessages(emailCredentials);
                default:
                case ServerProtocols.Imap:
                    return Imap.GetMessages(emailCredentials);
            }
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, int takeLast)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Pop.GetMessages(emailCredentials, takeLast);
        }

        public Task<IReadOnlyList<MimeMessage>> GetMessages(User user, string publicName, SearchQuery searchQuery)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Imap.GetMessages(emailCredentials, searchQuery);
        }

        public Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(User user)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<EmailDto>>(user.Emails));
        }

        public Task SendMessage(User user, string publicName, MimeMessage message)
        {
            EmailCredentials? emailCredentials = TryGetEmailCredentials(user, publicName);

            return Smtp.SendMessage(emailCredentials, message);
        }

        public async Task SetNewEmail(User user, EmailCredentials emailCredentials)
        {
            user.Emails.Add(emailCredentials);
            emailCredentials.UserId = user.Id;

            await _userRepository.UpdateAsync(user);
            await _emailRepository.UpdateAsync(emailCredentials);

            await _unitOfWork.SaveChangesAsync();
        }

        private EmailCredentials TryGetEmailCredentials(User user, string publicName)
        {
            EmailCredentials? emailCredentials = user.Emails.FirstOrDefault(e => e.PublicName == publicName);

            if (emailCredentials == null)
            {
                throw new ArgumentException("There is no email with this public name");
            }

            return emailCredentials;
        }
    }
}