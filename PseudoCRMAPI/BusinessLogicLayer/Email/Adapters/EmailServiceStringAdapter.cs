using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Email.Shared;
using Core;
using Core.Dtos;
using Core.Email;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Email.Adapters
{
    public class EmailServiceStringAdapter : EmailShared, IEmailService<string, EmailCredentials>
    {
        private readonly IEmailService<User, EmailCredentials> _emailService;

        public EmailServiceStringAdapter(IEmailService<User, EmailCredentials> emailService, IRepository<User> userRepository) : base(userRepository) 
        {
            _emailService = emailService;
        }

        public async Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(string user)
        {
            return await _emailService.GetRegisteredPublicNames(await GetUserById(user));
        }

        public async Task SetNewEmail(string user, EmailCredentials emailCredentials)
        {
            await _emailService.SetNewEmail(await GetUserById(user), emailCredentials);
        }
    }
}