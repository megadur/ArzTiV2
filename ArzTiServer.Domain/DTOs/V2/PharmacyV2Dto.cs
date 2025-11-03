namespace ArzTi3Server.Domain.DTOs.V2
{
    /// <summary>
    /// Enhanced pharmacy DTO for ARZ_TI 2.0 pharmacy management
    /// </summary>
    public class PharmacyV2Dto
    {
        public string IkNumber { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Owner { get; set; }
        
        // Status and permissions (Could Have - Priority C)
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public string? LoginId { get; set; }
        public List<string> AuthorizedUseCases { get; set; } = new(); // APO_TI use cases
        
        // Activity tracking (Could Have - Priority C)
        public DateTime? FirstDataTransmission { get; set; }
        public DateTime? LastDataTransmission { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Response wrapper for pharmacy list queries
    /// </summary>
    public class PharmacyListResponse
    {
        public IEnumerable<PharmacyV2Dto> Pharmacies { get; set; } = default!;
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public DateTime ResponseTimestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Request for creating or updating pharmacy information
    /// </summary>
    public class PharmacyCreateUpdateRequest
    {
        public string IkNumber { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Owner { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> AuthorizedUseCases { get; set; } = new();
    }

    /// <summary>
    /// Request for managing pharmacy login credentials
    /// </summary>
    public class PharmacyLoginRequest
    {
        public string IkNumber { get; set; } = default!;
        public string LoginId { get; set; } = default!;
        public string? Password { get; set; } // For password updates
        public List<string> AuthorizedUseCases { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }
}