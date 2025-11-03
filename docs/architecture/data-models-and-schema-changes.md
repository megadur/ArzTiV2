# Data Models and Schema Changes

### New Data Models

#### **PrescriptionQueryCache**
**Purpose:** Cache frequently accessed prescription queries to reduce database load  
**Integration:** Extends existing prescription entities without modifying core tables

**Key Attributes:**
- CacheKey: string - Composite key based on query parameters (type, pharmacy, date range)
- QueryHash: string - SHA256 hash of normalized query parameters  
- CachedResult: JsonDocument - Serialized prescription result set
- ExpirationTime: DateTime - Cache TTL for invalidation
- HitCount: int - Usage frequency for cache optimization
- LastAccessed: DateTime - LRU cache management

**Relationships:**
- **With Existing:** References prescription tables via foreign keys but doesn't modify them
- **With New:** Standalone caching layer with no direct entity relationships

#### **PrescriptionQueryPerformanceMetrics**
**Purpose:** Track query performance metrics for optimization and monitoring  
**Integration:** Captures performance data from existing controller operations

**Key Attributes:**
- QueryType: string - Type of operation (GetAll, GetByStatus, GetByPharmacy)
- ExecutionTimeMs: decimal - Query execution time in milliseconds
- RecordCount: int - Number of records returned
- TenantId: string - Multitenant identifier
- Timestamp: DateTime - Execution timestamp
- QueryParameters: JsonDocument - Normalized query parameters

**Relationships:**
- **With Existing:** Correlates with existing prescription operations via tenant context
- **With New:** Links to PrescriptionQueryCache for cache effectiveness analysis

#### **DatabaseIndexOptimization**
**Purpose:** Track and manage database index usage for query optimization  
**Integration:** Monitors existing prescription table query patterns

**Key Attributes:**
- TableName: string - Target table (er_senderezepte_emuster16, etc.)
- IndexName: string - PostgreSQL index identifier
- UsageCount: long - Index utilization frequency
- LastUsed: DateTime - Most recent index access
- EffectivenessScore: decimal - Performance improvement metric

**Relationships:**
- **With Existing:** References existing prescription tables for index monitoring
- **With New:** No direct relationships - operational metadata

### Schema Integration Strategy

**Database Changes Required:**
- **New Tables:** 
  - prescription_query_cache (performance optimization)
  - prescription_query_metrics (monitoring)
  - database_index_optimization (maintenance)
- **Modified Tables:** None - existing prescription schema preserved
- **New Indexes:** 
  - Composite indexes on TransferArz + Type + Timestamp for frequent queries
  - Pharmacy-based indexes on ApoIkNr for tenant-specific queries
  - Status-based indexes for prescription lifecycle tracking
- **Migration Strategy:** Additive-only migrations with zero downtime deployment

**Backward Compatibility:**
- All existing Entity Framework queries continue unchanged
- New performance layer operates alongside existing data access patterns
- Cache invalidation ensures data consistency with existing update operations
- Performance metrics collection transparent to existing business logic
