using BusinessLogicLayer.Abstractions.Email;
using Core;
using Core.Dtos;
using Core.Email;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Email.Adapters
{
    public class EmailServiceStringAdapter : IEmailService<string, EmailCredentials>
    {
        private readonly IEmailService<User, EmailCredentials> _emailService;
        private readonly IRepository<User> _userRepository;

        public EmailServiceStringAdapter(IEmailService<User, EmailCredentials> emailService, IRepository<User> userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(string user)
        {
            User? userObj = await _userRepository.ReadAsync(u => u.Name == user);
            if (userObj == null)
            {
                throw new ArgumentException("There is no user with this name");
            }

            return await _emailService.GetRegisteredPublicNames(userObj);
        }

        public async Task SetNewEmail(string user, EmailCredentials emailCredentials)
        {
            User? userObj = await _userRepository.ReadAsync(u => u.Name == user);
            if (userObj == null)
            {
                throw new ArgumentException("There is no user with this name");
            }

            await _emailService.SetNewEmail(userObj, emailCredentials);
        }
    }
}