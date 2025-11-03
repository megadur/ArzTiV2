# Tech Stack

### Existing Technology Stack

| Category | Current Technology | Version | Usage in Enhancement | Notes |
|----------|-------------------|---------|---------------------|--------|
| **Runtime** | .NET | 8.0 | Foundation platform | Mandatory for ARZ ecosystem compatibility |
| **Web Framework** | ASP.NET Core | 8.0 | Enhanced controllers & middleware | Existing PrescriptionsV2Controller extended |
| **Database ORM** | Entity Framework Core | 8.0.10 | Performance optimization layer | Current bottleneck - requires enhancement |
| **Database** | PostgreSQL (Npgsql) | 8.0.8 | High-performance queries | Connection pooling & optimization |
| **Authentication** | BCrypt.Net-Next | 4.0.3 | Maintained for compatibility | No changes required |
| **API Documentation** | Swashbuckle.AspNetCore | 6.8.1 | Enhanced performance metrics | Additional monitoring endpoints |
| **API Versioning** | Asp.Versioning.Mvc | 8.1.0 | Maintained v2 compatibility | No changes required |
| **Object Mapping** | AutoMapper | 13.0.1 | Performance-optimized mappings | Reduced allocation patterns |
| **Health Monitoring** | Microsoft.Extensions.Diagnostics.HealthChecks | 9.0.10 | Enhanced performance health checks | Database performance monitoring |

### New Technology Additions

| Technology | Version | Purpose | Rationale | Integration Method |
|------------|---------|---------|-----------|-------------------|
| **Microsoft.Extensions.Caching.Memory** | 8.0 | Query result caching | Reduce database load for frequently accessed prescriptions | Service layer integration |
| **System.Threading.Channels** | 8.0 | Async streaming for large datasets | Handle 1M+ record queries efficiently | Controller async enumerable responses |
| **Microsoft.Extensions.ObjectPool** | 8.0 | Database connection optimization | Improve multitenant connection pooling | DbContext factory enhancement |
| **Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite** | 8.0.8 | Spatial indexing (if geographical data) | Optimize pharmacy location queries | Optional - based on requirements analysis |
