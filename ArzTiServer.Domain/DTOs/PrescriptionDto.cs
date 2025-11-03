namespace ArzTi3Server.Domain.DTOs
{
    public class PrescriptionDto
    {
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
    }

    public class PrescriptionResponse
    {
        public IEnumerable<PrescriptionDto> Prescriptions { get; set; } = default!;
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
    }

    public class BulkOperationRequest
    {
        public IEnumerable<PrescriptionIdentifier> Prescriptions { get; set; } = default!;
    }

    public class PrescriptionIdentifier
    {
        public string Type { get; set; } = default!;
        public string RezeptUuid { get; set; } = default!;
    }

    public class BulkOperationResponse
    {
        public bool Success { get; set; }
        public int ProcessedCount { get; set; }
        public IEnumerable<string> Errors { get; set; } = default!;
    }
}