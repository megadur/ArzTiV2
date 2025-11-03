# API Design and Endpoints

### Performance-Enhanced Endpoints

#### **GET /v2/rezept (Enhanced)**
**Current Implementation:** Existing in PrescriptionsV2Controller  
**Performance Enhancement:** Internal query optimization without changing external interface

**Request Format (Unchanged):**
```http
GET /v2/rezept?type=eMuster16&page=1&pageSize=100
Authorization: Basic [credentials]
Accept: application/json
```

**Response Format (Unchanged):**
```json
{
  "prescriptions": [...],
  "totalCount": 1500,
  "page": 1,
  "pageSize": 100,
  "hasNextPage": true,
  "environment": "Production",
  "responseTimestamp": "2025-10-19T10:30:00Z"
}
```

**Internal Performance Enhancements:**
- **Query Caching:** 5-minute cache for frequently accessed prescription types
- **Optimized Database Queries:** Compiled Entity Framework queries with PostgreSQL-specific optimizations
- **Streaming Response:** Async enumerable for large result sets (1M+ records)
- **Connection Pooling:** Enhanced multitenant connection management

#### **GET /v2/rezept/status (Enhanced)**
**Current Implementation:** Pharmacy-specific prescription retrieval  
**Performance Enhancement:** Tenant-aware caching and optimized pharmacy queries

**Internal Optimizations:**
- **Pharmacy-Specific Caching:** Cache keyed by pharmacy ID and prescription type
- **Index Optimization:** Enhanced indexes on ApoIkNr for faster pharmacy-based queries
- **Bulk Loading:** Efficient batch retrieval for multiple prescription types

### New Performance Monitoring Endpoints

#### **GET /v2/performance/metrics**
**Purpose:** Performance monitoring and diagnostics (operational endpoint)  
**Access:** Administrative access only

**Response Format:**
```json
{
  "queryPerformance": {
    "averageResponseTimeMs": 850,
    "targetResponseTimeMs": 1000,
    "performanceImprovement": "88%",
    "cacheHitRate": "72%"
  },
  "systemMetrics": {
    "activeConnections": 15,
    "memoryUsageMB": 512,
    "queriesPerSecond": 25
  }
}
```

#### **GET /v2/health/performance**
**Purpose:** Enhanced health check with performance validation  
**Integration:** Extends existing health check infrastructure

**Response Format:**
```json
{
  "status": "Healthy",
  "performanceStatus": "Optimal",
  "checks": {
    "databasePerformance": {
      "status": "Healthy",
      "responseTime": "0.8s",
      "target": "<1.0s"
    },
    "cachePerformance": {
      "status": "Healthy",
      "hitRate": "75%",
      "target": ">60%"
    }
  }
}
```

### API Versioning Strategy

**Current Version Maintenance:**
- **v2.0:** All existing endpoints maintain full compatibility
- **Internal Versioning:** Performance optimizations implemented as internal service versions
- **Feature Flags:** Gradual rollout capability for performance enhancements

**Future Versioning Path:**
- **v2.1 (Optional):** If additional performance endpoints needed
- **Deprecation Strategy:** No deprecation planned - v2.0 remains stable
