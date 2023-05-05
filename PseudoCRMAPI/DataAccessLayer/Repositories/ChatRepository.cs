using Core.ChatEntities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class ChatRepository: EfRepository<Chat>
    {
        public ChatRepository(CrmDbContext dbContext):base(dbContext){}

        public Task<Chat?> ReadAsync(Expression<Func<Chat, bool>> predicate, int skip, int page)
        {
            return _context.Chats.Where(predicate).Include(chat => chat.Messages).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Chat>> ReadCollectionAsync(Expression<Func<Chat, bool>> predicate, int skip, int take, int page)
        {
            return Task.FromResult(_context.Chats.Where(predicate).Skip(skip).Take(take).Include(chat => chat.Messages).AsEnumerable());
        }
    }
}