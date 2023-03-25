using DataAccessLayer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EfUnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _context;
        public EfUnitOfWork(T context)
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