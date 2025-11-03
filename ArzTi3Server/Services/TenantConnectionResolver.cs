using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ArzTi3Server.Domain.Model.ArzSw;
using System.Linq;

namespace ArzTi3Server.Services
{
    public class TenantConnectionResolver : ITenantConnectionResolver
    {
        private readonly IMemoryCache _cache;
        private readonly string _arzSwConnection;
        private readonly ILogger<TenantConnectionResolver> _logger;

        public TenantConnectionResolver(
            IMemoryCache cache,
            IConfiguration configuration,
            ILogger<TenantConnectionResolver> logger)
        {
            _cache = cache;
            _logger = logger;
            _arzSwConnection = configuration.GetConnectionString("ArzSwConnection");

            if (string.IsNullOrEmpty(_arzSwConnection))
            {
                _logger.LogError("ArzSwConnection is not configured");
            }
        }

        public async Task<string?> ResolveForUserAsync(ClaimsPrincipal user)
        {
            if (user == null)
            {
                _logger.LogWarning("ResolveForUserAsync called with null user");
                return null;
            }

            // Prefer explicit claim names; check multiple possible claim keys
            var loginName = user.FindFirst("login_name")?.Value
                            ?? user.FindFirst("LoginName")?.Value
                            ?? user.FindFirst(ClaimTypes.Name)?.Value
                            ?? user.Identity?.Name;

            if (string.IsNullOrEmpty(loginName))
            {
                _logger.LogWarning("User does not contain a login name claim");
                return null;
            }

            // Try cache first
            if (_cache.TryGetValue<string>(GetCacheKey(loginName), out var cachedConn))
            {
                _logger.LogDebug("Tenant connection string found in cache for {LoginName}", loginName);
                return cachedConn;
            }

            if (string.IsNullOrEmpty(_arzSwConnection))
            {
                _logger.LogError("ArzSw connection string not configured; cannot resolve tenant for {LoginName}", loginName);
                return null;
            }

            try
            {
                // Create DbContext options on demand
                var options = new DbContextOptionsBuilder<ArzSwDbContext>()
                    .UseNpgsql(_arzSwConnection)
                    .Options;
                
                await using var ctx = new ArzSwDbContext(options);

                // Using the existing ArzswBenutzer and ArzswDatenbank relationship
                var tenantConn = await ctx.ArzswBenutzers
                    .Include(b => b.ArzswDatenbank)
                    .Where(b => b.LoginName == loginName)
                    .Select(b => b.ArzswDatenbank.DatenbankConnectionString)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(tenantConn))
                {
                    _logger.LogWarning("No tenant mapping found in arzsw_db for login {LoginName}", loginName);
                    return null;
                }

                // Cache result for a short period
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(GetCacheKey(loginName), tenantConn, cacheEntryOptions);

                _logger.LogInformation("Resolved tenant connection for {LoginName}", loginName);
                return tenantConn;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving tenant connection for {LoginName}", loginName);
                return null;
            }
        }

        // Simple direct lookup by tenant ID - for middleware
        public string GetConnectionString(string tenantId)
        {
            // Try to get from cache first
            string cacheKey = $"TenantConnection_{tenantId}";
            
            if (_cache.TryGetValue(cacheKey, out string connectionString))
            {
                return connectionString;
            }

            // Use existing ArzswDatenbank with code_kenner as tenantId
            var options = new DbContextOptionsBuilder<ArzSwDbContext>()
                .UseNpgsql(_arzSwConnection)
                .Options;
            
            using var ctx = new ArzSwDbContext(options);
            var tenant = ctx.ArzswMandants
                .Join(ctx.ArzswDatenbanks,
                    m => m.ArzswMandantId,
                    d => d.ArzswMandantId,
                    (m, d) => new { Mandant = m, Datenbank = d })
                .Where(x => x.Mandant.CodeKenner == tenantId && x.Datenbank.DatenbankAktiv == true)
                .Select(x => x.Datenbank)
                .FirstOrDefault();

            if (tenant == null)
            {
                throw new KeyNotFoundException($"Tenant with ID {tenantId} not found");
            }

            // Cache the connection string
            _cache.Set(cacheKey, tenant.DatenbankConnectionString, TimeSpan.FromHours(1));
            
            return tenant.DatenbankConnectionString;
        }

        private static string GetCacheKey(string loginName) => $"tenant_conn_{loginName}";
    }
}