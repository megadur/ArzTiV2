using System.Security.Claims;
using System.Threading.Tasks;

namespace ArzTi3Server.Services
{
    public interface ITenantConnectionResolver
    {
        /// <summary>
        /// Resolve tenant (database) connection string for the given user principal.
        /// Returns null if unable to resolve (missing claims or no mapping).
        /// </summary>
        Task<string?> ResolveForUserAsync(ClaimsPrincipal user);
    }
}