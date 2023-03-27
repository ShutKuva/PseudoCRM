using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Email.Protocols;
using Core;
using Core.Email;
using Core.Email.Additional;
using DataAccessLayer.Abstractions;
using MailKit.Search;
using MimeKit;

namespace BusinessLogicLayer.Email
{
    public class EmailService : IEmailService<User, MimeMessage, SearchQuery, EmailCredentials>
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<EmailCredentials, int> _emailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IRepository<User, int> userRepository, IRepository<EmailCredentials, int> emailRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
            _unitOfWork = unitOfWork;
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

        private Task<IEnumerable<MailboxAddress>> ConvertAddresses(IEnumerable<string> emails)
        {
            return Task.FromResult(emails.Select(e => new MailboxAddress("", e)));
        }
    }
}