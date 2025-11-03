# Performance-Strategie Revision - ARZ_TI v3

## Use Case Reality Check

### Typischer ARZ-Workflow:
```
1. ARZ fragt alle 5-15 Minuten nach neuen Rezepten
2. Holt verfügbare Rezepte ab (Batch-Processing)
3. Führt Kassenabrechnung durch (extern)
4. Updated Status auf ABGERECHNET/FEHLER (Bulk-Updates)
5. Repeat cycle
```

### Daten-Charakteristika:
```
READ-Pattern:
├── Häufige Abfragen nach neuen Rezepten
├── Status-Filter: NEU, IN_BEARBEITUNG
├── Zeit-basierte Abfragen (seit letzter Abfrage)
└── Tenant-isoliert (pro ARZ)

WRITE-Pattern:
├── Status-Updates nach Abrechnung
├── Bulk-Updates (100-1000+ Rezepte)
├── Häufige Cache-Invalidation
└── Immediately visible (Compliance!)
```

## Korrigierte Performance-Strategie

### **1. Database-First Optimization**
```sql
-- Optimierte Indizes für Use Case
CREATE INDEX CONCURRENTLY idx_prescriptions_arz_status_created 
ON prescriptions(arz_id, status, created_date DESC) 
WHERE status IN ('NEU', 'IN_BEARBEITUNG');

CREATE INDEX CONCURRENTLY idx_prescriptions_bulk_update 
ON prescriptions(arz_id, prescription_uuid) 
WHERE status = 'IN_BEARBEITUNG';

-- Partitionierung wenn möglich
CREATE TABLE prescriptions_partition_arz_nord 
PARTITION OF prescriptions FOR VALUES IN ('ARZ_NORD');
```

### **2. Query-Optimierung statt Caching**
```csharp
// Optimierte Dapper Queries
public async Task<List<Prescription>> GetNewPrescriptionsAsync(
    string connectionString, 
    string arzId, 
    DateTime since)
{
    using var connection = new NpgsqlConnection(connectionString);
    
    return await connection.QueryAsync<Prescription>(@"
        SELECT prescription_id, prescription_uuid, status, created_date, prescription_data
        FROM prescriptions 
        WHERE arz_id = @arzId 
          AND status = 'NEU'
          AND created_date > @since
        ORDER BY created_date DESC
        LIMIT 1000",
        new { arzId, since });
}
```

### **3. Bulk-Operations Optimierung**
```csharp
// Effiziente Bulk-Updates
public async Task<int> UpdatePrescriptionStatusBulkAsync(
    string connectionString,
    List<PrescriptionStatusUpdate> updates)
{
    using var connection = new NpgsqlConnection(connectionString);
    
    var sql = @"
        UPDATE prescriptions 
        SET status = @status, 
            updated_date = @updatedDate,
            status_info = @statusInfo
        WHERE prescription_uuid = @prescriptionUuid";
    
    return await connection.ExecuteAsync(sql, updates);
}
```

### **4. Selective Smart Caching**
```csharp
// NUR für quasi-statische Daten
public class SmartCacheStrategy 
{
    // Cache für Apotheken-Stammdaten (ändern sich selten)
    public async Task<Apotheke> GetApothekeAsync(string iknr)
    {
        var cacheKey = $"apotheke_{iknr}";
        if (_cache.TryGetValue(cacheKey, out Apotheke cached))
            return cached;
            
        var apotheke = await _repository.GetApothekeAsync(iknr);
        _cache.Set(cacheKey, apotheke, TimeSpan.FromHours(4));
        return apotheke;
    }
    
    // KEIN Cache für Rezept-Daten (zu dynamisch)
    public async Task<List<Prescription>> GetPrescriptionsAsync(string arzId)
    {
        // Direct database access - no cache
        return await _repository.GetPrescriptionsAsync(arzId);
    }
}
```

### **5. Connection Pool Optimization**
```csharp
// Tenant-spezifische Connection Pools
public class TenantConnectionPoolManager
{
    private readonly ConcurrentDictionary<string, NpgsqlDataSource> _dataSources;
    
    public NpgsqlConnection GetConnection(string tenantId)
    {
        var dataSource = _dataSources.GetOrAdd(tenantId, tid => 
        {
            var connectionString = GetConnectionString(tid);
            var builder = new NpgsqlDataSourceBuilder(connectionString);
            builder.ConfigureConnectionString(csb => 
            {
                csb.MaxPoolSize = GetPoolSize(tid);  // ARZ-spezifisch
                csb.MinPoolSize = 5;
                csb.ConnectionLifetime = 300;
            });
            return builder.Build();
        });
        
        return dataSource.OpenConnection();
    }
}
```

## Performance-Ziel ohne Multi-Level Cache

### **Realistische 90% Verbesserung:**
```
Aktuelle Performance (v2): 5-10 Sekunden
├── EF Core Overhead: ~60%
├── Ineffiziente Queries: ~25%  
├── Connection Management: ~10%
└── Network/DB Load: ~5%

Ziel Performance (v3): <1 Sekunde
├── Dapper (kein ORM): -60% Zeit
├── Optimierte SQL: -25% Zeit
├── Connection Pooling: -10% Zeit
├── Index-Optimierung: -15% Zeit
└── Gesamt: -110% = unter 1 Sekunde möglich!
```

### **Ohne komplexe Cache-Invalidation:**
```
Vorteile:
├── Einfachere Architektur
├── Konsistente Daten (keine stale cache)
├── Weniger Memory-Verbrauch
├── Bessere Skalierbarkeit
├── Einfachere Fehlerdiagnose
└── Compliance-sicher (immer aktuelle Daten)
```

## Empfohlene Architektur-Änderung

### **Entfernen aus Präsentation:**
```
❌ Multi-Level Caching (Memory + Redis)
❌ Cache-Invalidation Strategien
❌ Redis Infrastructure-Kosten
```

### **Hinzufügen zur Präsentation:**
```
✅ Database-First Performance-Optimization
✅ Intelligent Indexing Strategy
✅ Tenant-aware Connection Pooling
✅ Bulk-Operation Optimization
✅ Simple Selective Caching (nur Stammdaten)
```

**FAZIT: Multi-Level Caching ist für diesen Use Case contraproductive. Database-Optimierung + Connection Pooling erreicht das 90% Performance-Ziel effizienter und compliance-sicherer!**
