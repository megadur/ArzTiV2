# Epic Technical Specification: Epic 1: Grundlagen & Kern-Infrastruktur

Date: 2025-11-03
Author: Megadur
Epic ID: 1
Status: Draft

---

## Overview

Epic 1 establishes the foundational infrastructure for the ARZ_TI 3 brownfield enhancement, implementing core authentication, multitenancy, and basic prescription retrieval capabilities. This epic transforms the existing ARZ system into a high-performance API platform capable of handling 1M+ prescription records while maintaining secure tenant isolation. The implementation focuses on leveraging existing database schemas and ARZ infrastructure to provide immediate value through new prescription access patterns.

## Objectives and Scope

**In Scope:**
- .NET 8.0 Web API project structure with proper solution organization
- Basic Authentication against existing ArzSw database for ARZ system credentials
- Multitenant connection management with dynamic DbContext creation
- Core prescription retrieval API (GET /v2/rezept) supporting all three prescription types
- OpenAPI documentation generation and basic monitoring capabilities
- Integration with existing PostgreSQL schemas without structural changes

**Out of Scope:**
- Advanced performance optimizations (covered in Epic 4)
- Complex prescription management operations (covered in Epics 2-3)
- Full monitoring and alerting infrastructure (basic health checks only)
- UI components or client SDKs
- Database schema modifications or new table creation

## System Architecture Alignment

This epic aligns with the brownfield enhancement strategy by building upon existing ARZ infrastructure without requiring database migrations or schema changes. The implementation leverages the current ArzSw authentication database and prescription tables while introducing a new service layer for optimized access patterns. The multitenant architecture respects existing client isolation patterns, and the API versioning strategy (v2) allows for coexistence with current ARZ systems during the transition period.

## Detailed Design

### Services and Modules

| Service/Module | Responsibility | Input | Output | Owner |
|---|---|---|---|---|
| **BasicAuthenticationHandler** | Validate ARZ system credentials against ArzSw database | Authorization header with Basic auth | ClaimsPrincipal with tenant info | Authentication Layer |
| **TenantConnectionMiddleware** | Resolve client-specific database connections | X-Tenant-Id header or auth context | DbContext with tenant connection | Middleware Layer |
| **MultitenantDbContextFactory** | Create tenant-specific DbContext instances | Connection string, tenant ID | Configured DbContext | Data Layer |
| **PrescriptionsV2Controller** | Handle prescription API requests with versioning | HTTP requests to /v2/rezept | JSON prescription responses | API Layer |
| **PrescriptionRepository** | Data access for prescription operations | Query parameters, filters | Prescription entities | Data Layer |
| **TenantService** | Manage tenant configuration and validation | Tenant identifiers | Tenant metadata and settings | Business Layer |

### Data Models and Contracts

**Core Prescription Entity (Existing Schema):**
```csharp
public class Prescription
{
    public Guid UUID { get; set; }
    public string XMLRequest { get; set; }
    public bool TransferArz { get; set; }  // Filter: false = new prescriptions
    public int PrescriptionType { get; set; }  // 1=eMuster16, 2=P-Rezept, 3=E-Rezept
    public string ApoIkNr { get; set; }  // Pharmacy identifier for tenant isolation
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

**API Response Contract:**
```csharp
public class PrescriptionResponse
{
    public List<PrescriptionDto> Prescriptions { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
    public string Environment { get; set; }
    public DateTime ResponseTimestamp { get; set; }
}
```

### APIs and Interfaces

**Primary API Endpoint:**
```http
GET /v2/rezept
Authorization: Basic {base64-encoded-credentials}
X-Tenant-Id: {pharmacy-identifier}
Accept: application/json

Query Parameters:
- type: string (eMuster16|P-Rezept|E-Rezept) - Optional filter
- page: integer (default: 1) - Page number for pagination
- pageSize: integer (default: 100, max: 1000) - Records per page
```

**Service Interfaces:**
```csharp
public interface ITenantConnectionResolver
{
    Task<string> ResolveConnectionStringAsync(string tenantId);
}

public interface IMultitenantDbContextFactory
{
    Task<ArzTiDbContext> CreateDbContextAsync(string tenantId);
}

public interface ITenantService
{
    Task<bool> ValidateTenantAsync(string tenantId);
    Task<TenantConfiguration> GetTenantConfigAsync(string tenantId);
}
```

### Workflows and Sequencing

**Authentication & Authorization Flow:**
1. Client sends request with Basic Authorization header
2. BasicAuthenticationHandler extracts and validates credentials against ArzSw DB
3. Handler creates ClaimsPrincipal with tenant information
4. TenantConnectionMiddleware resolves tenant-specific connection string
5. Request proceeds to controller with authenticated context

**Prescription Retrieval Sequence:**
1. PrescriptionsV2Controller receives GET /v2/rezept request
2. Controller validates query parameters and pagination limits
3. MultitenantDbContextFactory creates tenant-specific DbContext
4. PrescriptionRepository executes filtered query (TransferArz=false)
5. Results paginated and transformed to PrescriptionDto format
6. Response includes metadata (total count, pagination info)
7. Proper HTTP status codes returned (200, 400, 401, 500)

## Non-Functional Requirements

### Performance

**Target Response Times:**
- API endpoint response time: < 2 seconds for queries up to 1000 records
- Authentication validation: < 200ms per request
- Database connection establishment: < 500ms per tenant
- Pagination queries: < 1 second for standard page sizes (100 records)

**Throughput Requirements:**
- Support minimum 50 concurrent requests per ARZ system
- Handle up to 10,000 prescription records per query response
- Maintain performance with 1M+ total prescription records in database

### Security

**Authentication Requirements:**
- Basic Authentication using existing ArzSw database credentials
- Credential validation against encrypted password storage
- Failed authentication attempts logged for security monitoring
- No credential caching beyond request lifecycle for Epic 1

**Authorization & Tenant Isolation:**
- Strict tenant isolation - clients can only access their own prescription data
- Connection string resolution based on validated tenant identity
- No cross-tenant data leakage through query optimization
- Proper HTTP 401 responses for authentication failures, 403 for authorization failures

**Data Protection:**
- All prescription data transmission over HTTPS only
- No sensitive data logging in application logs
- Prescription XML content treated as confidential medical data

### Reliability/Availability

**Availability Targets:**
- 99.5% uptime during business hours (6 AM - 10 PM CET)
- Graceful degradation when tenant databases are unavailable
- Circuit breaker pattern for database connection failures

**Error Handling:**
- Comprehensive exception handling with proper HTTP status codes
- Database connection failures return 503 Service Unavailable
- Invalid query parameters return 400 Bad Request with clear error messages
- Timeout handling for long-running queries (30 second timeout)

### Observability

**Basic Logging Requirements:**
- Request/response logging for all API calls
- Authentication success/failure events
- Database connection and query performance metrics
- Error logs with correlation IDs for debugging

**Health Monitoring:**
- Basic health check endpoint (GET /health)
- Database connectivity verification in health checks
- Response time monitoring for performance baseline establishment
- Memory and CPU usage basic monitoring

## Dependencies and Integrations

**Framework Dependencies:**
- .NET 8.0 (LTS) - Core runtime and ASP.NET Core Web API
- Microsoft.EntityFrameworkCore.Design 8.0.10 - ORM for database operations
- Npgsql.EntityFrameworkCore.PostgreSQL 8.0.8 - PostgreSQL database provider
- Microsoft.Extensions.Diagnostics.HealthChecks 9.0.10 - Health monitoring
- BCrypt.Net-Next 4.0.3 - Password hashing for authentication
- Swashbuckle.AspNetCore 6.8.1 - OpenAPI documentation generation
- Asp.Versioning.Mvc 8.1.0 - API versioning support

**External System Integrations:**
- **ArzSw Database** - Authentication credential storage and validation
- **Tenant PostgreSQL Databases** - Prescription data storage per pharmacy
- **Existing ARZ Systems** - API consumers requiring prescription data access

**Internal Project References:**
- **ArzTi3Server.Domain** - Shared entity models and repository contracts
- **Existing Database Schemas** - Leverages current prescription table structures without modifications

**Integration Constraints:**
- No database schema changes permitted - must work with existing table structures
- Backward compatibility with current ARZ system authentication methods
- Connection pooling must handle multiple tenant databases efficiently
- API versioning allows coexistence with existing ARZ API endpoints

## Acceptance Criteria (Authoritative)

**Story 1.1: Project Setup and Basic Authentication**
1. .NET 8.0 Web API project with proper solution structure created
2. Basic Authentication middleware implemented and configured
3. Authentication handler validates credentials against ARZ system database
4. All API endpoints require authentication
5. Proper HTTP status codes for authentication errors returned (401 Unauthorized)
6. Integration tests verify authentication success and failure scenarios

**Story 1.2: Multitenant Capability and Connection Management**
7. Client identification mechanism implemented (via header or authentication)
8. ArzSw database integration for connection string resolution
9. Dynamic DbContext creation with resolved connection strings
10. Tenant isolation verified - clients can only access their own data
11. Connection pooling optimized for multitenant scenarios
12. Error handling for invalid or missing tenant configurations

**Story 1.3: Basic Prescription Retrieval API**
13. GET /v2/rezept endpoint implemented with versioning
14. Prescription filtering by TransferArz=false (new prescriptions only)
15. Support for all three prescription types (eMuster16, P-Rezept, E-Rezept)
16. Basic pagination implemented (page and pageSize parameters)
17. Prescription data includes essential fields: UUID, XML request data, type identifiers
18. Response includes proper metadata (total count, pagination info)
19. Error handling and logging for all failure scenarios

**Story 1.4: OpenAPI Documentation and Basic Monitoring**
20. OpenAPI 3.x specification generated and accessible via Swagger UI
21. All endpoints documented with parameters, responses, and examples
22. Authentication requirements clearly documented
23. Basic request/response logging implemented
24. Health check endpoint available for monitoring
25. Environment configuration (Test/Staging/Live) clearly identified in responses

## Traceability Mapping

| AC# | Spec Section | Component/API | Test Idea |
|-----|-------------|---------------|-----------|
| 1 | Services and Modules | ArzTi3Server project structure | Verify project builds and runs |
| 2-3 | Security Architecture | BasicAuthenticationHandler | Test valid/invalid credentials |
| 4 | APIs and Interfaces | [Authorize] attribute on controllers | Test unauthenticated requests return 401 |
| 5 | Non-Functional Requirements | Authentication error responses | Verify HTTP status codes |
| 6 | Test Strategy | Integration test suite | Authentication flow tests |
| 7-8 | Data Models | TenantConnectionMiddleware | Test tenant header processing |
| 9 | Services and Modules | MultitenantDbContextFactory | Test connection string resolution |
| 10 | Security Requirements | Tenant isolation logic | Cross-tenant access prevention tests |
| 11 | Performance Requirements | Connection pooling configuration | Load test with multiple tenants |
| 12 | Reliability Requirements | Error handling middleware | Test invalid tenant scenarios |
| 13 | APIs and Interfaces | PrescriptionsV2Controller.Get | Endpoint versioning tests |
| 14 | Data Models | TransferArz filter logic | Test prescription filtering |
| 15 | Data Models | PrescriptionType enumeration | Test all prescription type filters |
| 16 | APIs and Interfaces | Pagination parameters | Test page/pageSize validation |
| 17 | Data Models | PrescriptionDto mapping | Test response field mapping |
| 18 | APIs and Interfaces | PrescriptionResponse metadata | Test pagination metadata |
| 19 | Observability Requirements | Exception handling | Error scenario tests |
| 20-21 | Dependencies | Swashbuckle configuration | Swagger UI accessibility test |
| 22 | APIs and Interfaces | OpenAPI security definitions | Documentation completeness review |
| 23 | Observability Requirements | Request/response logging | Log output verification |
| 24 | Observability Requirements | Health check endpoint | Health endpoint functionality test |
| 25 | APIs and Interfaces | Environment identification | Environment header verification |

## Risks, Assumptions, Open Questions

**Risks:**
- **Risk:** Database connection pool exhaustion under high concurrent load from multiple tenants
  - **Mitigation:** Implement connection pool monitoring and configure appropriate pool limits per tenant
- **Risk:** Performance degradation with large prescription datasets (1M+ records)
  - **Mitigation:** Implement query timeouts and pagination limits; defer optimization to Epic 4
- **Risk:** Authentication database unavailability causing complete service outage
  - **Mitigation:** Implement credential caching and graceful degradation patterns

**Assumptions:**
- **Assumption:** Existing ArzSw database schema contains necessary credential validation fields
  - **Next Step:** Verify database schema compatibility during Story 1.1 implementation
- **Assumption:** Current PostgreSQL prescription table structure supports filtering requirements
  - **Next Step:** Validate TransferArz field availability and prescription type mapping
- **Assumption:** ARZ systems can adapt to Basic Authentication with minimal integration changes
  - **Next Step:** Coordinate with ARZ system teams for authentication testing

**Open Questions:**
- **Question:** What are the exact field names in existing prescription tables for tenant isolation?
  - **Next Step:** Database schema analysis during Story 1.2 development
- **Question:** Should pagination default to 100 or 50 records per page for optimal performance?
  - **Next Step:** Performance testing during Story 1.3 to determine optimal page size
- **Question:** What level of request logging is required for compliance and debugging?
  - **Next Step:** Clarify logging requirements with operations team

## Test Strategy Summary

**Unit Testing Framework:**
- xUnit for .NET testing with comprehensive controller and service layer coverage
- Repository pattern testing with in-memory database for data layer validation
- Authentication handler testing with mocked credential scenarios

**Integration Testing Approach:**
- Full API endpoint testing with TestServer and real database connections
- Multitenant scenarios with multiple test databases
- Authentication flow testing with valid/invalid credential combinations
- Pagination and filtering validation across all prescription types

**Performance Testing:**
- Load testing with 50 concurrent requests per tenant (minimum requirement)
- Database query performance validation with 10,000+ prescription records
- Connection pooling stress testing under multitenant load
- Response time validation against NFR targets (< 2 seconds)

**Security Testing:**
- Tenant isolation validation - ensure no cross-tenant data access
- Authentication bypass attempts and proper 401/403 response validation
- HTTPS-only enforcement testing
- Credential validation boundary testing (SQL injection, malformed headers)

**Acceptance Testing:**
- End-to-end scenarios matching each story's acceptance criteria
- OpenAPI documentation validation and Swagger UI functionality
- Health check endpoint verification under various system states
- Environment configuration testing (Test/Staging/Live identification)

