using Core.Email;
using Core;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Email.Shared
{
    public abstract class EmailShared
    {
        private readonly IRepository<User> _userRepository;

        public EmailShared(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        protected EmailCredentials TryGetEmailCredentials(User user, string publicName)
        {
            EmailCredentials? emailCredentials = user.Emails.FirstOrDefault(e => e.PublicName == publicName);

            if (emailCredentials == null)
            {
                throw new ArgumentException("There is no email with this public name");
            }

            return emailCredentials;
        }

        protected async Task<User> GetUserById(string user)
        {
            User? userObj = await _userRepository.ReadAsync(u => u.Id == int.Parse(user));

            if (userObj == null)
            {
                throw new ArgumentException("There is no user with this id");
            }

            return userObj;
        }
    }
}