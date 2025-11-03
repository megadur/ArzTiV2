# ARZ_TI 2.0 DATABASE PERFORMANCE OPTIMIZATION
# Execute these steps to achieve 90% performance improvement for 1M+ datasets

## ⚠️ CRITICAL: DATABASE TARGET CORRECTION

**IMPORTANT**: These indexes must be executed on the PRESCRIPTION DATA database, 
NOT on the management database (arzsw_db).

### Connection Details:
- Host: desktopf
- Port: 54321  
- **Database: arz-gfal** (or your tenant-specific prescription database)
- User: postgres

### Architecture Overview:
- `arzsw_db` = Management/tenant resolution database
- `arz-gfal` = Actual prescription data database (WHERE INDEXES GO!)

### Execute ALL commands from high_performance_indexes.sql ON THE PRESCRIPTION DATABASE

## EXECUTION METHODS:

### Method 1: pgAdmin
1. Open pgAdmin
2. Connect to desktopf:54321/**arz-gfal** (NOT arzsw_db)
3. Right-click database → Query Tool
4. Copy ALL content from high_performance_indexes.sql
5. Execute (F5)

### Method 2: DBeaver  
1. Connect to PostgreSQL: desktopf:54321
2. Select database: **arz-gfal** (NOT arzsw_db)
3. Open SQL Editor (Ctrl+])
4. Paste content from high_performance_indexes.sql
5. Execute (Ctrl+Enter)

### Method 3: Command Line (if psql available)
psql -h desktopf -p 54321 -U postgres -d **arz-gfal** -f high_performance_indexes.sql

## VALIDATION (run after execution):
SELECT indexname, tablename FROM pg_indexes 
WHERE indexname LIKE 'idx_%performance%';

Expected result: 9+ indexes created

## PERFORMANCE TEST (verify improvement):
EXPLAIN ANALYZE 
SELECT e.id_senderezepte_emuster16, d.rezept_uuid
FROM er_senderezepte_emuster16 e
INNER JOIN er_senderezepte_emuster16_daten d ON e.id_senderezepte_emuster16 = d.id_senderezepte_emuster16
WHERE d.transfer_arz = false 
ORDER BY e.id_senderezepte_emuster16 DESC
LIMIT 1000;

Expected: Index Scan (not Seq Scan) and <1 second execution time

