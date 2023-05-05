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

        public override Task<User?> ReadAsync(Expression<Func<User, bool>> predicate, int skip, int page)
        {
            return _context.Users
                .Include(user => user.Organization)
                .ThenInclude(org => org.Chat)
                .ThenInclude(chat => chat.Messages)
                .Include(user => user.Emails)
                .ThenInclude(email => email.ServerInformations)
                .ThenInclude(si => si.ServerInformation)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public override Task<IEnumerable<User>> ReadCollectionAsync(Expression<Func<User, bool>> predicate, int skip, int take, int page)
        {
            return Task.FromResult(_context.Users
                .Include(user => user.Emails)
                .ThenInclude(email => email.ServerInformations)
                .ThenInclude(si => si.ServerInformation)
                .Include(user => user.Organization)
                .ThenInclude(org => org.Chat)
                .ThenInclude(chat => chat.Messages)
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .AsEnumerable());
        }
    }
}