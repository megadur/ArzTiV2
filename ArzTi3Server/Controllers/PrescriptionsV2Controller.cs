using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArzTi3Server.Domain.DTOs.V2;
using ArzTi3Server.Services;
using ArzTi3Server.Domain.Repositories;
using Asp.Versioning;
using System.Security.Claims;

namespace ArzTi3Server.Controllers
{
    /// <summary>
    /// ARZ_TI 2.0 Prescription Management Controller
    /// High-performance API for prescription retrieval and bulk status management
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/rezept")]
    [Authorize]
    public class PrescriptionsV2Controller : ControllerBase
    {
        private readonly ILogger<PrescriptionsV2Controller> _logger;
        private readonly IMultitenantDbContextFactory _dbContextFactory;
        private readonly IConfiguration _configuration;

        public PrescriptionsV2Controller(
            ILogger<PrescriptionsV2Controller> logger,
            IMultitenantDbContextFactory dbContextFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// GET /v2/rezept - Retrieve all new prescriptions by type across all pharmacies
        /// Must Have (M) - Core functionality for ARZ_TI 2.0
        /// </summary>
        /// <param name="type">Prescription type filter (eMuster16, P-Rezept, E-Rezept)</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Records per page (default: 100, max configurable per environment)</param>
        /// <returns>Paginated list of new prescriptions with enhanced metadata</returns>
        [HttpGet]
        public async Task<ActionResult<PrescriptionV2Response>> GetAllNewPrescriptions(
            [FromQuery] string? type = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100)
        {
            try
            {
                _logger.LogInformation("ARZ_TI 2.0: GetAllNewPrescriptions called - Type: {Type}, Page: {Page}, PageSize: {PageSize}", 
                    type, page, pageSize);

                // Validate and get client context
                var connectionString = User.FindFirst("ConnectionString")?.Value;
                var clientCode = User.FindFirst("ClientCode")?.Value;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    _logger.LogWarning("ARZ_TI 2.0: Missing client connection string");
                    return BadRequest("Client connection string not found");
                }

                // Environment-specific max page size
                var maxPageSize = _configuration.GetValue<int>("Performance:MaxPageSize", 1000);
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                    _logger.LogWarning("ARZ_TI 2.0: PageSize limited to {MaxPageSize} for performance", maxPageSize);
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                // High-performance retrieval with type filtering
                var prescriptions = await repository.GetNewPrescriptionsV2Async(page, pageSize, type);
                var totalCount = await repository.GetNewPrescriptionsCountV2Async(type);
                
                var response = new PrescriptionV2Response
                {
                    Prescriptions = prescriptions,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    HasNextPage = (page * pageSize) < totalCount,
                    Environment = _configuration.GetValue<string>("Environment", "Development"),
                    ResponseTimestamp = DateTime.UtcNow
                };

                _logger.LogInformation("ARZ_TI 2.0: Retrieved {Count} prescriptions (Total: {Total})", 
                    prescriptions.Count(), totalCount);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ARZ_TI 2.0: Error retrieving prescriptions - Type: {Type}", type);
                return StatusCode(500, "Internal server error occurred while retrieving prescriptions");
            }
        }

        /// <summary>
        /// GET /v2/rezept/status - Retrieve all new prescriptions for specific pharmacy
        /// Must Have (M) - Pharmacy-specific prescription management
        /// </summary>
        /// <param name="pharmacyId">Pharmacy identifier for filtering</param>
        /// <param name="type">Prescription type filter</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Records per page</param>
        /// <returns>Pharmacy-specific prescriptions with enhanced error information</returns>
        [HttpGet("status")]
        public async Task<ActionResult<PrescriptionV2Response>> GetPharmacyPrescriptions(
            [FromQuery] string? pharmacyId = null,
            [FromQuery] string? type = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100)
        {
            try
            {
                _logger.LogInformation("ARZ_TI 2.0: GetPharmacyPrescriptions called - Pharmacy: {PharmacyId}, Type: {Type}", 
                    pharmacyId, type);

                var connectionString = User.FindFirst("ConnectionString")?.Value;
                var clientCode = User.FindFirst("ClientCode")?.Value;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    return BadRequest("Client connection string not found");
                }

                // Use pharmacy from auth context if not provided
                if (string.IsNullOrEmpty(pharmacyId))
                {
                    pharmacyId = User.FindFirst("PharmacyId")?.Value;
                }

                var maxPageSize = _configuration.GetValue<int>("Performance:MaxPageSize", 1000);
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                // Pharmacy-specific retrieval with enhanced error information
                var prescriptions = await repository.GetPharmacyPrescriptionsV2Async(pharmacyId, page, pageSize, type);
                var totalCount = await repository.GetPharmacyPrescriptionsCountV2Async(pharmacyId, type);
                
                var response = new PrescriptionV2Response
                {
                    Prescriptions = prescriptions,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    HasNextPage = (page * pageSize) < totalCount,
                    Environment = _configuration.GetValue<string>("Environment", "Development"),
                    ResponseTimestamp = DateTime.UtcNow
                };

                _logger.LogInformation("ARZ_TI 2.0: Retrieved {Count} pharmacy prescriptions (Pharmacy: {PharmacyId})", 
                    prescriptions.Count(), pharmacyId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ARZ_TI 2.0: Error retrieving pharmacy prescriptions - Pharmacy: {PharmacyId}", pharmacyId);
                return StatusCode(500, "Internal server error occurred while retrieving pharmacy prescriptions");
            }
        }

        /// <summary>
        /// PATCH /v2/rezept/statusuuid - Bulk status retrieval with error information
        /// Must Have (M) - High-performance bulk status queries with comprehensive error reporting
        /// </summary>
        /// <param name="request">List of prescription UUIDs to retrieve status for</param>
        /// <returns>Bulk status information with detailed error tracking</returns>
        [HttpPatch("statusuuid")]
        public async Task<ActionResult<BulkStatusResponse>> GetMultiplePrescriptionStatus(
            [FromBody] BulkStatusRequest request)
        {
            try
            {
                _logger.LogInformation("ARZ_TI 2.0: GetMultiplePrescriptionStatus called for {Count} UUIDs", 
                    request.RezeptUuids.Count);

                if (request?.RezeptUuids == null || !request.RezeptUuids.Any())
                {
                    return BadRequest("RezeptUuids list cannot be empty");
                }

                var connectionString = User.FindFirst("ConnectionString")?.Value;
                if (string.IsNullOrEmpty(connectionString))
                {
                    return BadRequest("Client connection string not found");
                }

                // Limit bulk operations for performance
                var maxBulkSize = _configuration.GetValue<int>("Performance:MaxBulkSize", 1000);
                if (request.RezeptUuids.Count > maxBulkSize)
                {
                    return BadRequest($"Bulk operation limited to {maxBulkSize} items");
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                // High-performance bulk status retrieval
                var statusResults = await repository.GetBulkPrescriptionStatusV2Async(request.RezeptUuids);
                
                var response = new BulkStatusResponse
                {
                    Prescriptions = statusResults.Where(s => s != null).Select(p => new PrescriptionStatusDto
                    {
                        RezeptUuid = p.RezeptUuid ?? string.Empty,
                        Type = p.Type,
                        TransferArz = p.TransferArz,
                        Status = p.CheckLevel,
                        RegelTrefferCode = p.RegelTrefferCode,
                        CheckLevel = p.CheckLevel,
                        LastUpdated = p.StatusAbfrageDatum
                    }).ToList(),
                    TotalCount = request.RezeptUuids.Count,
                    SuccessCount = statusResults.Count(s => s != null),
                    ErrorCount = request.RezeptUuids.Count - statusResults.Count(s => s != null),
                    GlobalErrors = new List<string>(),
                    ResponseTimestamp = DateTime.UtcNow
                };

                // Add errors for not found UUIDs
                var foundUuids = statusResults.Where(s => s != null).Select(s => s.RezeptUuid).ToHashSet();
                var notFoundUuids = request.RezeptUuids.Except(foundUuids);
                foreach (var uuid in notFoundUuids)
                {
                    response.GlobalErrors.Add($"Prescription not found: {uuid}");
                }

                _logger.LogInformation("ARZ_TI 2.0: Bulk status query completed - Success: {Success}, Errors: {Errors}", 
                    response.SuccessCount, response.ErrorCount);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ARZ_TI 2.0: Error in bulk status retrieval");
                return StatusCode(500, "Internal server error occurred during bulk status retrieval");
            }
        }

        /// <summary>
        /// PATCH /v2/rezept/status - Bulk update prescription statuses
        /// Must Have (M) - Efficient bulk status updates with transaction management
        /// </summary>
        /// <param name="request">Bulk update request with prescription UUIDs and target statuses</param>
        /// <returns>Bulk update results with success/error tracking</returns>
        [HttpPatch("status")]
        public async Task<ActionResult<BulkUpdateResponse>> UpdateMultiplePrescriptionStatus(
            [FromBody] BulkUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("ARZ_TI 2.0: UpdateMultiplePrescriptionStatus called for {Count} prescriptions", 
                    request.Updates.Count);

                if (request?.Updates == null || !request.Updates.Any())
                {
                    return BadRequest("Updates list cannot be empty");
                }

                var connectionString = User.FindFirst("ConnectionString")?.Value;
                if (string.IsNullOrEmpty(connectionString))
                {
                    return BadRequest("Client connection string not found");
                }

                var maxBulkSize = _configuration.GetValue<int>("Performance:MaxBulkSize", 1000);
                if (request.Updates.Count > maxBulkSize)
                {
                    return BadRequest($"Bulk operation limited to {maxBulkSize} items");
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                // Execute bulk update with transaction management
                var updates = request.Updates.Select(u => new PrescriptionStatusUpdate
                {
                    RezeptUuid = u.RezeptUuid,
                    Type = u.Type,
                    TransferArz = u.TransferArz,
                    Status = u.Status
                }).ToList();
                
                var updateResult = await repository.BulkUpdatePrescriptionStatusV2Async(updates);
                
                var response = new BulkUpdateResponse
                {
                    Success = updateResult.OverallSuccess,
                    ProcessedCount = request.Updates.Count,
                    SuccessCount = updateResult.SuccessCount,
                    ErrorCount = updateResult.ErrorCount,
                    Errors = updateResult.Errors,
                    ResponseTimestamp = DateTime.UtcNow
                };

                _logger.LogInformation("ARZ_TI 2.0: Bulk update completed - Success: {Success}, Errors: {Errors}", 
                    response.SuccessCount, response.ErrorCount);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ARZ_TI 2.0: Error in bulk status update");
                return StatusCode(500, "Internal server error occurred during bulk status update");
            }
        }
    }
}