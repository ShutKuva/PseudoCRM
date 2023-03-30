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
    public class EmailService : IEmailService<User, EmailCredentials>
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

        public Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(User user)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<EmailDto>>(user.Emails));
        }

        public async Task SetNewEmail(User user, EmailCredentials emailCredentials)
        {
            user.Emails.Add(emailCredentials);
            emailCredentials.UserId = user.Id;

            await _userRepository.UpdateAsync(user);
            await _emailRepository.UpdateAsync(emailCredentials);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}