using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArzTi3Server.Services
{
    public class SystemMonitoringOptions
    {
        public int MonitoringIntervalSeconds { get; set; } = 60;
        public long MemoryThresholdBytes { get; set; } = 1024 * 1024 * 1024; // 1GB
        public bool MonitorCpu { get; set; } = true;
        public bool MonitorMemory { get; set; } = true;
        public string LogFilePath { get; set; } = "system_monitoring.log";
    }

    public class SystemMonitoringService : BackgroundService
    {
        private readonly IOptions<SystemMonitoringOptions> _options;

        public SystemMonitoringService(IOptions<SystemMonitoringOptions> options)
        {
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Monitor system resources
                    var totalMemory = GC.GetTotalMemory(false);
                    
                    // Log memory usage
                    Console.WriteLine($"[{DateTime.UtcNow}] Memory usage: {totalMemory / (1024 * 1024)} MB");

                    // Check against threshold
                    if (totalMemory > _options.Value.MemoryThresholdBytes)
                    {
                        Console.WriteLine($"[{DateTime.UtcNow}] WARNING: Memory usage exceeds threshold!");
                    }

                    // Additional monitoring logic can be implemented here
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.UtcNow}] Error in system monitoring: {ex.Message}");
                }

                // Wait for the next monitoring interval
                await Task.Delay(TimeSpan.FromSeconds(_options.Value.MonitoringIntervalSeconds), stoppingToken);
            }
        }
    }
}