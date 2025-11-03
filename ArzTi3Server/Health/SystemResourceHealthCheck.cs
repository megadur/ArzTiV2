using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ArzTi3Server.Health;

public class SystemResourceHealthCheck : IHealthCheck
{
    private readonly SystemMonitoringService _monitoringService;
    private readonly IOptions<SystemMonitoringOptions> _options;

    public SystemResourceHealthCheck(
        SystemMonitoringService monitoringService,
        IOptions<SystemMonitoringOptions> options)
    {
        _monitoringService = monitoringService;
        _options = options;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        var totalMemory = GC.GetTotalMemory(false);
        var memoryUsageMB = totalMemory / (1024 * 1024);
        
        var data = new Dictionary<string, object>
        {
            { "totalMemoryBytes", totalMemory },
            { "memoryUsageMB", memoryUsageMB },
            { "thresholdMB", _options.Value.MemoryThresholdBytes / (1024 * 1024) }
        };

        if (totalMemory > _options.Value.MemoryThresholdBytes)
        {
            return Task.FromResult(
                HealthCheckResult.Degraded(
                    $"Memory usage is high: {memoryUsageMB} MB", 
                    data: data));
        }

        return Task.FromResult(
            HealthCheckResult.Healthy(
                $"Memory usage is healthy: {memoryUsageMB} MB", 
                data: data));
    }
}