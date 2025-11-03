using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ArzTi3Server.Services;
using ArzTi3Server.Domain.Model.ArzSw;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq;

namespace ArzTi3Server.Tests
{
    // Test-specific implementation that can use in-memory context
    public class TestableT﻿enantConnectionResolver : ITenantConnectionResolver
    {
        private readonly IMemoryCache _cache;
        private readonly DbContextOptions<ArzSwDbContext> _options;
        private readonly ILogger<TenantConnectionResolver> _logger;

        public TestableT﻿enantConnectionResolver(
            IMemoryCache cache,
            DbContextOptions<ArzSwDbContext> options,
            ILogger<TenantConnectionResolver> logger)
        {
            _cache = cache;
            _options = options;
            _logger = logger;
        }

        public async Task<string?> ResolveForUserAsync(ClaimsPrincipal user)
        {
            if (user == null)
            {
                _logger.LogWarning("ResolveForUserAsync called with null user");
                return null;
            }

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

            try
            {
                await using var ctx = new ArzSwDbContext(_options);

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

        private static string GetCacheKey(string loginName) => $"tenant_conn_{loginName}";
    }

    public class TenantConnectionResolverTests
    {
        private DbContextOptions<ArzSwDbContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ArzSwDbContext>()
                .UseInMemoryDatabase(databaseName: "arzsw_test_db")
                .Options;
        }

        [Fact]
        public async Task ResolveForUserAsync_Returns_ConnectionString_And_Caches()
        {
            var options = CreateInMemoryOptions();

            // Seed data
            await using (var ctx = new ArzSwDbContext(options))
            {
                var db = new ArzswDatenbank 
                { 
                    ArzswDatenbankId = 1, 
                    DatenbankConnectionString = "Host=tenantdb;Database=tenant1;",
                    DatenbankName = "TestDatabase",
                    DatenbankAktiv = true,
                    ArzswMandantId = 1
                };
                var user = new ArzswBenutzer 
                { 
                    ArzswBenutzerId = 1, 
                    LoginName = "testuser", 
                    BenutzerName = "Test User",
                    LoginPasswort = "testpassword",
                    ArzswDatenbank = db, 
                    ArzswMandantId = 1,
                    ArzswDatenbankId = 1
                };
                ctx.ArzswDatenbanks?.Add(db);
                ctx.ArzswBenutzers?.Add(user);
                await ctx.SaveChangesAsync();
            }

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var logger = new NullLogger<TenantConnectionResolver>();

            var resolver = new TestableT﻿enantConnectionResolver(memoryCache, options, logger);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("login_name", "testuser") }, "test"));

            var conn = await resolver.ResolveForUserAsync(claims);
            Assert.Equal("Host=tenantdb;Database=tenant1;", conn);

            // Now update DB to a different connection and ensure cached value is returned
            await using (var ctx = new ArzSwDbContext(options))
            {
                var dbEntry = ctx.ArzswDatenbanks.First();
                dbEntry.DatenbankConnectionString = "Host=changed;Database=tenantX;";
                await ctx.SaveChangesAsync();
            }

            var cached = await resolver.ResolveForUserAsync(claims);
            Assert.Equal("Host=tenantdb;Database=tenant1;", cached); // cached original
        }
    }
}
