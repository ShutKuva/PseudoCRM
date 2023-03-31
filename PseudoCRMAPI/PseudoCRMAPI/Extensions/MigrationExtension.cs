using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace PseudoCRMAPI.Extensions
{
    public static class MigrationExtension
    {
        public static WebApplication UseMigration(this WebApplication host)
        {
            using IServiceScope scope =  host.Services.CreateScope();
            CrmDbContext dbContext = scope.ServiceProvider.GetRequiredService<CrmDbContext>();
            dbContext.Database.Migrate();
            return host;
        }
    }
}
