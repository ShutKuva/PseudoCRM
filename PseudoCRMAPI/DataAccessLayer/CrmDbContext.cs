using Core;
using Core.Email;
using Core.Email.Additional;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CrmDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmailCredentials> EmailCredentials { get; set; }

        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
        {
            
        }

        public CrmDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailCredentialsServerInformation>()
                .HasKey(ecsr => new {ecsr.EmailCredentialsId, ecsr.ServerInformationId});

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZHEKA;Database=Crm;Trusted_Connection=True;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}