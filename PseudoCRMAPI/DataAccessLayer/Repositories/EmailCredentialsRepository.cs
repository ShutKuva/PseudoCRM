using System.Linq.Expressions;
using Core.Email;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EmailCredentialsRepository : EfRepository<EmailCredentials>
    {
        public EmailCredentialsRepository(CrmDbContext context) : base(context)
        {
        }

        public override Task<EmailCredentials?> ReadAsync(Expression<Func<EmailCredentials, bool>> predicate, int skip, int take)
        {
            return _context.EmailCredentials.Include(emailCred => emailCred.ServerInformations).Where(predicate).FirstOrDefaultAsync();
        }

        public override Task<IEnumerable<EmailCredentials>> ReadCollectionAsync(Expression<Func<EmailCredentials, bool>> predicate, int skip, int take, int page)
        {
            return Task.FromResult(_context.EmailCredentials.Include(emailCred => emailCred.ServerInformations).Where(predicate).Skip(skip).Take(take).AsEnumerable());
        }
    }
}