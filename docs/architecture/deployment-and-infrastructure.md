# Deployment and Infrastructure

### Current Infrastructure Analysis

**Existing Deployment Pattern:**
- **Platform:** IIS on Windows Server with AspNetCoreModuleV2
- **Runtime:** .NET 8.0 with in-process hosting
- **Database:** PostgreSQL with connection pooling
- **Configuration:** Environment-specific appsettings files (Development, Test, Staging, Production, Live)
- **Automation:** PowerShell deployment scripts with comprehensive diagnostics

### Performance-Enhanced Deployment Strategy

#### **Zero-Downtime Deployment Approach**
**Strategy:** Blue-Green deployment with performance validation checkpoints

**Deployment Phases:**
1. **Performance Component Deployment:** Deploy new performance services alongside existing code
2. **Feature Flag Activation:** Gradual enablement of performance optimizations
3. **Performance Validation:** Automated performance testing before full activation
4. **Rollback Capability:** Instant fallback to existing performance patterns

#### **Enhanced Infrastructure Configuration**

**Updated appsettings.Production.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "ArzTi3Server.Performance": "Information",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "Performance": {
    "EnableOptimizations": true,
    "CacheProvider": "Memory",
    "MaxPageSize": 5000,
    "DefaultPageSize": 100,
    "QueryTimeoutSeconds": 30,
    "EnableQueryResultCaching": true,
    "CacheExpirationMinutes": 5,
    "MaxConcurrentRequests": 50,
    "EnableAsyncStreaming": true,
    "DatabaseConnectionPoolSize": 30,
    "EnablePerformanceMetrics": true,
    "PerformanceMetricsRetentionDays": 30
  },
  "PerformanceLimits": {
    "DefaultTenant": {
      "QueryTimeoutMs": 3000,
      "MaxResultSetSize": 10000,
      "QueriesPerMinute": 120
    },
    "PremiumTenant": {
      "QueryTimeoutMs": 5000,
      "MaxResultSetSize": 50000,
      "QueriesPerMinute": 600
    }
  }
}
```

### Environment-Specific Deployment

#### **Development Environment**
```json
{
  "Performance": {
    "EnableOptimizations": false,
    "EnableDetailedLogging": true,
    "CacheExpirationMinutes": 1,
    "QueryTimeoutSeconds": 60
  }
}
```

#### **Test Environment**
```json
{
  "Performance": {
    "EnableOptimizations": true,
    "EnablePerformanceMetrics": true,
    "CacheExpirationMinutes": 2,
    "MaxConcurrentRequests": 10
  }
}
```

#### **Staging Environment**
```json
{
  "Performance": {
    "EnableOptimizations": true,
    "EnablePerformanceMetrics": true,
    "CacheExpirationMinutes": 5,
    "MaxConcurrentRequests": 25,
    "EnableLoadTesting": true
  }
}
```

#### **Production Environment**
```json
{
  "Performance": {
    "EnableOptimizations": true,
    "EnablePerformanceMetrics": true,
    "CacheExpirationMinutes": 5,
    "MaxConcurrentRequests": 50,
    "EnableAlerts": true,
    "PerformanceTargetMs": 1000
  }
}
```

### Database Migration Strategy

#### **Performance Schema Deployment**
**Migration Approach:** Additive-only database changes

```sql
-- Performance enhancement tables (deployed separately)
CREATE TABLE IF NOT EXISTS prescription_query_cache (
    cache_key VARCHAR(255) PRIMARY KEY,
    tenant_id VARCHAR(100) NOT NULL,
    cached_result JSONB NOT NULL,
    expiration_time TIMESTAMP NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_prescription_cache_tenant_expiration 
ON prescription_query_cache(tenant_id, expiration_time);

-- Performance metrics table
CREATE TABLE IF NOT EXISTS prescription_query_metrics (
    id SERIAL PRIMARY KEY,
    tenant_id VARCHAR(100) NOT NULL,
    query_type VARCHAR(50) NOT NULL,
    execution_time_ms DECIMAL(10,2) NOT NULL,
    record_count INTEGER NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Disaster Recovery and Rollback

#### **Rollback Strategy**
**Immediate Rollback Capability:**
1. **Feature Flag Disable:** Instant disable of performance optimizations
2. **Configuration Rollback:** Revert to previous appsettings configuration
3. **Application Rollback:** Previous version deployment ready for instant activation
4. **Database Rollback:** Performance tables can be dropped without affecting core functionality
