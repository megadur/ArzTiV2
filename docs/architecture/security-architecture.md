# Security Architecture

## Overview
For the ARZ_TI 3 greenfield API serving a **closed audience** (internal pharmaceutical systems), Basic Authentication provides adequate security while maximizing performance. This approach eliminates JWT token processing overhead and simplifies the security architecture.

### Current Security Foundation Analysis

**Existing Security Components:**
- **BasicAuthenticationHandler:** ARZ system credential validation against ArzSw database
- **TenantConnectionMiddleware:** X-Tenant-Id header-based tenant resolution
- **Multitenant Isolation:** Dynamic DbContext creation with tenant-specific connection strings
- **Claims-Based Authorization:** Connection string and client context stored in user claims

## Basic Authentication Strategy

### **Optimized BasicAuthenticationHandler**
**Status:** Enhanced for high-performance closed-network operation

**Performance-Focused Enhancements:**
- **Credential Caching:** In-memory credential validation cache (5-minute TTL)
- **Connection Pool Optimization:** Dedicated authentication database connections
- **Fast-Path Validation:** Optimized database queries for credential verification
- **Performance Monitoring:** Authentication timing metrics for performance tracking

```csharp
public class OptimizedBasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IMemoryCache _credentialCache;
    private readonly IConfiguration _config;
    private readonly ILogger<OptimizedBasicAuthenticationHandler> _logger;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        var authHeader = Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.NoResult();

        var credentials = ExtractCredentials(authHeader);
        var cacheKey = $"auth:{HashCredentials(credentials)}";

        // Fast credential cache lookup
        if (_credentialCache.TryGetValue(cacheKey, out ClaimsPrincipal cachedPrincipal))
        {
            return AuthenticateResult.Success(new AuthenticationTicket(cachedPrincipal, Scheme.Name));
        }

        // Database validation with optimized query
        var validationResult = await ValidateCredentialsAsync(credentials);
        if (validationResult.IsValid)
        {
            var principal = CreateClaimsPrincipal(validationResult);
            _credentialCache.Set(cacheKey, principal, TimeSpan.FromMinutes(5));
            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }

        return AuthenticateResult.Fail("Invalid credentials");
    }
}
```

## Tenant Isolation Security

### **High-Performance TenantConnectionMiddleware**
**Status:** Optimized for closed-network, Basic Auth environment

**Performance-First Security Features:**
- **Fast Tenant Resolution:** In-memory tenant mapping with cache preloading
- **Connection Pool Optimization:** Pre-warmed, tenant-specific connection pools
- **Resource Isolation:** CPU and memory limits per tenant to prevent resource starvation
- **Query Complexity Protection:** Automatic prevention of expensive cross-tenant queries

```csharp
public class HighPerformanceTenantMiddleware
{
    private readonly IMemoryCache _tenantCache;
    private readonly IConnectionPoolManager _poolManager;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        if (string.IsNullOrEmpty(tenantId))
        {
            context.Response.StatusCode = 400;
            return;
        }

        // Fast tenant validation from cache
        var tenant = await GetTenantFromCacheAsync(tenantId);
        if (tenant == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        // Pre-warmed connection pool assignment
        context.Items["TenantConnectionString"] = tenant.ConnectionString;
        context.Items["TenantId"] = tenantId;

        await next(context);
    }
}
```

## Data Security & Performance

### **Query Security**
**Optimized for Closed Network:** Focus on performance while maintaining data isolation

**Security Implementations:**
- **Parameterized Queries:** All Dapper and EF queries use parameterized inputs
- **Tenant Data Filtering:** Automatic tenant-scoped queries prevent cross-tenant access
- **Result Set Limits:** Configurable pagination to prevent memory exhaustion
- **Query Timeout Protection:** Hard timeouts prevent runaway queries

### **Multi-Layer Cache Security**
**Performance-Focused:** Secure caching with minimal overhead

**Cache Security Strategy:**
- **Tenant-Isolated Keys:** Cache keys include tenant hash for isolation
- **Memory-First Approach:** L1 cache (in-memory) for maximum performance
- **Selective Encryption:** Only PII data encrypted in L2/L3 cache layers
- **Automatic Invalidation:** Time-based and event-driven cache invalidation

```csharp
public class SecureHighPerformanceCache
{
    public async Task<T> GetAsync<T>(string key, string tenantId)
    {
        var secureKey = $"{tenantId}:{HashKey(key)}";
        
        // L1: In-memory (fastest)
        if (_memoryCache.TryGetValue(secureKey, out T cachedValue))
            return cachedValue;
            
        // L2: Redis with selective encryption
        var redisValue = await _redis.GetAsync(secureKey);
        if (redisValue != null)
        {
            var decrypted = IsPersonalData(key) ? 
                Decrypt(redisValue) : redisValue;
            return JsonSerializer.Deserialize<T>(decrypted);
        }
        
        return default(T);
    }
}
```

## Network Security (Closed Environment)

### **Internal Network Protection**
**Assumption:** Closed pharmaceutical network with controlled access

**Security Measures:**
- **Network Segmentation:** API accessible only from designated internal networks
- **Connection Encryption:** TLS 1.3 for all communications (even internal)
- **IP Whitelisting:** Restrict access to known pharmaceutical system IP ranges
- **Basic Auth over HTTPS:** Simple, effective authentication for closed networks

### **Monitoring & Intrusion Detection**
**Lightweight Security:** Focused on performance anomalies rather than complex threat detection

**Monitoring Strategy:**
- **Performance-Based Alerting:** Unusual query patterns or response times
- **Authentication Failure Tracking:** Multiple failed attempts from same source
- **Resource Usage Monitoring:** Unusual CPU/memory consumption patterns
- **Query Pattern Analysis:** Detection of potentially malicious query structures

## German Pharmaceutical Compliance

### **GDPR & Pharmaceutical Regulations**
**Optimized Compliance:** Maintain regulatory compliance with minimal performance impact

**Compliance Implementation:**
- **Data Minimization:** Queries retrieve only necessary prescription fields
- **Audit Trail Efficiency:** Lightweight logging that doesn't impact performance
- **Right to Deletion:** Fast cache invalidation supports deletion requirements
- **Processing Transparency:** Clear documentation of all data processing operations

### **Security Configuration**

```json
{
  "Security": {
    "BasicAuth": {
      "CredentialCacheMinutes": 5,
      "MaxFailedAttempts": 3,
      "LockoutMinutes": 15
    },
    "TenantIsolation": {
      "QueryTimeoutSeconds": 30,
      "MaxResultSetSize": 1000,
      "MemoryLimitMB": 100
    },
    "Cache": {
      "EncryptPersonalData": true,
      "MaxCacheSizeMB": 512,
      "DefaultTTLMinutes": 60
    }
  }
}
```

## Security Performance Benefits

**Simplified Authentication:** Basic Auth eliminates JWT processing overhead
**Reduced Complexity:** Fewer security layers mean fewer potential failure points  
**Cache-Friendly:** Simple credentials enable effective authentication caching
**Network Optimized:** Designed for trusted internal pharmaceutical networks
**Compliance Maintained:** Meets German pharmaceutical regulations with minimal overhead

