using Core;
using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : EfRepository<User>
    {
        public UserRepository(CrmDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<User?> ReadAsync(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Include(user => user.Emails)
                .ThenInclude(email => email.ServerInformations)
                .ThenInclude(si => si.ServerInformation)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public override IEnumerable<User> ReadByCondition(Expression<Func<User, bool>> predicate, int skip, int take)
        {
            return _context.Users.Include(user => user.Emails)
                .ThenInclude(email => email.ServerInformations)
                .ThenInclude(si => si.ServerInformation)
                .Where(predicate)
                .Skip(skip)
                .Take(take);
        }

        public override Task<IEnumerable<User>> ReadByConditionAsync(Expression<Func<User, bool>> predicate, int skip, int take)
        {
            return Task.FromResult(_context.Users.Include(user => user.Emails)
                .ThenInclude(email => email.ServerInformations)
                .ThenInclude(si => si.ServerInformation)
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .AsEnumerable());
        }
    }
}