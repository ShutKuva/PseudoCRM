using AutoMapper;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Email.Shared;
using Core;
using Core.Dtos.Email;
using Core.Email;
using Core.Email.Additional;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Email.Adapters
{
    public class EmailServiceStringAdapter : EmailShared, IEmailService<string, EmailCredentialsDto, ServerInformation>
    {
        private readonly IEmailService<User, EmailCredentials, ServerInformation> _emailService;
        private readonly IMapper _mapper;

        public EmailServiceStringAdapter(IEmailService<User, EmailCredentials, ServerInformation> emailService, IMapper mapper, IRepository<User> userRepository) : base(userRepository)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<bool> CheckServerInfoAvailability(string user, EmailCredentialsDto emailCredentials, ServerInformation serverInfo)
        {
            User userObj = await GetUserById(user);
            EmailCredentials emailCredentialsObj = TryGetEmailCredentials(userObj, emailCredentials.PublicName);
            return await _emailService.CheckServerInfoAvailability(userObj, emailCredentialsObj, serverInfo);
        }

        public async Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(string user)
        {
            return await _emailService.GetRegisteredPublicNames(await GetUserById(user));
        }

        public async Task SetNewEmail(string user, EmailCredentialsDto emailCredentials)
        {
            await _emailService.SetNewEmail(await GetUserById(user), _mapper.Map<EmailCredentials>(emailCredentials));
        }

        public async Task SetNewServerInfo(string user, EmailCredentialsDto emailCredentials, ServerInformation serverInfo)
        {
            await _emailService.SetNewServerInfo(await GetUserById(user),
                _mapper.Map<EmailCredentials>(emailCredentials), serverInfo);
        }
    }
}