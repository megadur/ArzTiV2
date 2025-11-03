-- ARZ_TI 2.0 High-Performance Database Indexes
-- Optimized for 1M+ prescription dataset processing
-- Execute these indexes for significant performance improvements

-- ====================================================================
-- CRITICAL PERFORMANCE INDEXES FOR PRESCRIPTION RETRIEVAL
-- ====================================================================

-- eMuster16 Performance Indexes
-- Composite index for transfer_arz filtering with ID for efficient joins
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_emuster16_transfer_performance 
ON er_senderezepte_emuster16_daten(transfer_arz, id_senderezepte_emuster16) 
WHERE transfer_arz = false;

-- Status filtering for ABGERECHNET exclusion
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_emuster16_status_performance 
ON er_senderezepte_emuster16_status(id_senderezepte_emuster16, rezept_status) 
WHERE rezept_status != 'ABGERECHNET';

-- Primary entity ordering and lookup
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_emuster16_id_ordering 
ON er_senderezepte_emuster16(id_senderezepte_emuster16 DESC);

-- P-Rezept Performance Indexes  
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_prezept_transfer_performance 
ON er_senderezepte_prezept_daten(transfer_arz, id_senderezepte_prezept) 
WHERE transfer_arz = false;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_prezept_status_performance 
ON er_senderezepte_prezept_status(id_senderezepte_prezept, rezept_status) 
WHERE rezept_status != 'ABGERECHNET';

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_prezept_id_ordering 
ON er_senderezepte_prezept(id_senderezepte_prezept DESC);

-- E-Rezept Performance Indexes
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_erezept_transfer_performance 
ON er_senderezepte_erezept_daten(transfer_arz, id_senderezepte_erezept) 
WHERE transfer_arz = false;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_erezept_status_performance 
ON er_senderezepte_erezept_status(id_senderezepte_erezept, rezept_status) 
WHERE rezept_status != 'ABGERECHNET';

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_erezept_id_ordering 
ON er_senderezepte_erezept(id_senderezepte_erezept DESC);

-- ====================================================================
-- PHARMACY-SPECIFIC PERFORMANCE INDEXES 
-- ====================================================================

-- Pharmacy filtering for pharmacy-specific queries
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_emuster16_pharmacy_performance 
ON er_senderezepte_emuster16(rz_liefer_id, id_senderezepte_emuster16 DESC) 
WHERE rz_liefer_id IS NOT NULL;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_prezept_pharmacy_performance 
ON er_senderezepte_prezept(transaktions_nummer, id_senderezepte_prezept DESC) 
WHERE transaktions_nummer IS NOT NULL;

-- ====================================================================
-- BULK OPERATIONS PERFORMANCE INDEXES
-- ====================================================================

-- UUID-based lookups for status updates
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_emuster16_uuid_lookup 
ON er_senderezepte_emuster16_daten(rezept_uuid) 
WHERE rezept_uuid IS NOT NULL;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_prezept_uuid_lookup 
ON er_senderezepte_prezept_daten(rezept_uuid) 
WHERE rezept_uuid IS NOT NULL;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_erezept_uuid_lookup 
ON er_senderezepte_erezept_daten(rezept_uuid) 
WHERE rezept_uuid IS NOT NULL;

-- ====================================================================
-- MONITORING AND MAINTENANCE
-- ====================================================================

-- Analyze tables after index creation for optimal query planning
ANALYZE er_senderezepte_emuster16;
ANALYZE er_senderezepte_emuster16_daten;
ANALYZE er_senderezepte_emuster16_status;
ANALYZE er_senderezepte_prezept;
ANALYZE er_senderezepte_prezept_daten;
ANALYZE er_senderezepte_prezept_status;
ANALYZE er_senderezepte_erezept;
ANALYZE er_senderezepte_erezept_daten;
ANALYZE er_senderezepte_erezept_status;

-- Performance validation queries
-- Execute these to verify index effectiveness:

/*
-- Test query performance for eMuster16 (should use indexes)
EXPLAIN ANALYZE 
SELECT e.id_senderezepte_emuster16, e.rz_liefer_id, d.rezept_uuid
FROM er_senderezepte_emuster16 e
INNER JOIN er_senderezepte_emuster16_daten d ON e.id_senderezepte_emuster16 = d.id_senderezepte_emuster16
LEFT JOIN er_senderezepte_emuster16_status s ON e.id_senderezepte_emuster16 = s.id_senderezepte_emuster16
WHERE d.transfer_arz = false 
    AND (s.rezept_status IS NULL OR s.rezept_status != 'ABGERECHNET')
ORDER BY e.id_senderezepte_emuster16 DESC
LIMIT 1000;
*/

-- Expected Results After Index Creation:
-- - Query execution time: <1 second for 1000 records
-- - Index Scan instead of Seq Scan in EXPLAIN output
-- - Significant reduction in database CPU usage
-- - Improved concurrent user capacity (50+ users)