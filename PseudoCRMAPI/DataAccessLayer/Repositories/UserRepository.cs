using Core;
using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : EfRepository<User>, IRepository<User>
    {
        public UserRepository(CrmDbContext dbContext) : base(dbContext)
        {
        }

        public new Task<User?> ReadAsync(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Include(user => user.Emails).Where(predicate).FirstOrDefaultAsync();
        }

        public IEnumerable<User> ReadByCondition(Expression<Func<User, bool>> predicate, int skip, int take)
        {
            return _context.Users.Include(user => user.Emails).Where(predicate).Skip(skip).Take(take);
        }

        public Task<IEnumerable<User>> ReadByConditionAsync(Expression<Func<User, bool>> predicate, int skip, int take)
        {
            return Task.FromResult(_context.Users.Include(user => user.Emails).Where(predicate).Skip(skip).Take(take).AsEnumerable());
        }
    }
}