using Core;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CrmDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}