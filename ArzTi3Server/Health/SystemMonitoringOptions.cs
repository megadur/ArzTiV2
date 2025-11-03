namespace ArzTi3Server.Health;

public class SystemMonitoringOptions
{
    public long MemoryThresholdBytes { get; set; } = 1024 * 1024 * 500; // 500 MB default
}