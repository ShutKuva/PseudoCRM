using BusinessLogicLayer.Abstractions.Chat;
using Core;
using System.Linq.Expressions;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Chat
{
    public class UserService : IUserService<User>
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> ReadAsync(Expression<Func<User, bool>> predicate, int skip, int page)
        {
            return _userRepository.ReadAsync(predicate, 0, 0);
        }

        public Task<IEnumerable<User>> ReadCollectionAsync(Expression<Func<User, bool>> predicate, int skip, int take, int page)
        {
            return _userRepository.ReadCollectionAsync(predicate, skip, take, page);
        }
    }
}