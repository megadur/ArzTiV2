using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace ArzTi3Server.Health
{
    internal class TenantDatabaseHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Implement your health check logic here
            // For example, check tenant database connectivity, etc.
            return Task.FromResult(HealthCheckResult.Healthy("Tenant database is healthy."));
        }
    }
}