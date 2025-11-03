namespace ArzTi3Server.Domain.DTOs.V2
{
    /// <summary>
    /// Enhanced prescription DTO for ARZ_TI 2.0 with additional status tracking and error information
    /// </summary>
    public class PrescriptionV2Dto
    {
        // Core prescription fields (existing from v1)
        public string Type { get; set; } = default!;
        public int Id { get; set; }
        public string? RezeptUuid { get; set; }
        public string? XmlRequest { get; set; }
        public bool TransferArz { get; set; }
        public string? RzLieferId { get; set; }
        public long? Muster16Id { get; set; }
        public long? TransaktionsNummer { get; set; }
        public string? ErezeptId { get; set; }
        public int? DatenId { get; set; }

        // Enhanced fields for ARZ_TI 2.0 (Should Have - Priority S)
        public string? RegelTrefferCode { get; set; }
        public string? CheckLevel { get; set; }
        public DateTime? StatusAbfrageDatum { get; set; }
        public DateTime? StatusAbfrageZeit { get; set; }
        public DateTime? DeliveryTimestamp { get; set; }
        public AvsSystemDto? AvsSystem { get; set; }
    }

    /// <summary>
    /// AVS System information for prescription tracking
    /// </summary>
    public class AvsSystemDto
    {
        public string? Manufacturer { get; set; }
        public string? Name { get; set; }
        public string? Version { get; set; }
    }

    /// <summary>
    /// Response wrapper for prescription queries with pagination and metadata
    /// </summary>
    public class PrescriptionV2Response
    {
        public IEnumerable<PrescriptionV2Dto> Prescriptions { get; set; } = default!;
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public string? Environment { get; set; } // Test, Staging, Live
        public DateTime ResponseTimestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Request for bulk status operations
    /// </summary>
    public class BulkStatusRequest
    {
        public List<string> RezeptUuids { get; set; } = new();
    }

    /// <summary>
    /// Individual prescription status information
    /// </summary>
    public class PrescriptionStatusDto
    {
        public string RezeptUuid { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool TransferArz { get; set; }
        public string? Status { get; set; }
        public string? RegelTrefferCode { get; set; }
        public string? CheckLevel { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
        public DateTime? LastUpdated { get; set; }
    }

    /// <summary>
    /// Response for bulk status queries
    /// </summary>
    public class BulkStatusResponse
    {
        public List<PrescriptionStatusDto> Prescriptions { get; set; } = new();
        public int TotalCount { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> GlobalErrors { get; set; } = new();
        public DateTime ResponseTimestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Request for bulk prescription status updates
    /// </summary>
    public class BulkUpdateRequest
    {
        public List<PrescriptionUpdateDto> Updates { get; set; } = new();
    }

    /// <summary>
    /// Individual prescription update information
    /// </summary>
    public class PrescriptionUpdateDto
    {
        public string RezeptUuid { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool? TransferArz { get; set; }
        public string? Status { get; set; } // e.g., "ABGERECHNET"
    }

    /// <summary>
    /// Response for bulk update operations
    /// </summary>
    public class BulkUpdateResponse
    {
        public bool Success { get; set; }
        public int ProcessedCount { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime ResponseTimestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Individual prescription status update for repository operations
    /// </summary>
    public class PrescriptionStatusUpdate
    {
        public string RezeptUuid { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool? TransferArz { get; set; }
        public string? Status { get; set; }
        public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Result from bulk update operations at repository level
    /// </summary>
    public class BulkUpdateResult
    {
        public bool OverallSuccess { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> SuccessfulUuids { get; set; } = new();
    }
}