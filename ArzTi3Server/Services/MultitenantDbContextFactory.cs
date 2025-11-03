using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Services
{
    public class MultitenantDbContextFactory : IMultitenantDbContextFactory
    {
        public ArzTiDbContext CreateDbContext(string connectionString)
        {
            var options = new DbContextOptionsBuilder<ArzTiDbContext>()
                .UseNpgsql(connectionString)
                .Options;
            
            return new ArzTiDbContext(options);
        }
    }
}