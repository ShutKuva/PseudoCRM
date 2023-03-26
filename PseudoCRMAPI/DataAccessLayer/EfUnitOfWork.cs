using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly CrmDbContext _context;
        public EfUnitOfWork(CrmDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}