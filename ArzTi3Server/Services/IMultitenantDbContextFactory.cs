using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Services
{
    public interface IMultitenantDbContextFactory
    {
        ArzTiDbContext CreateDbContext(string connectionString);
    }
}