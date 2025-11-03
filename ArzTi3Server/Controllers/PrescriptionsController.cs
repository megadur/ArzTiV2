using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArzTi3Server.Domain.DTOs;
using ArzTi3Server.Services;
using System.Security.Claims;
using ArzTi3Server.Domain.Repositories;
using Asp.Versioning;

namespace ArzTi3Server.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PrescriptionsController : ControllerBase
    {
        private readonly ILogger<PrescriptionsController> _logger;
        private readonly IMultitenantDbContextFactory _dbContextFactory;

        public PrescriptionsController(
            ILogger<PrescriptionsController> logger,
            IMultitenantDbContextFactory dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet("new")]
        public async Task<ActionResult<PrescriptionResponse>> GetNewPrescriptions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100,
            [FromQuery] string? rezeptType = null)
        {
            try
            {
                var connectionString = User.FindFirst("ConnectionString")?.Value;
                var clientCode = User.FindFirst("ClientCode")?.Value;
                
                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(clientCode))
                {
                    _logger.LogWarning("Unauthorized access attempt - missing connection string or client code");
                    return Unauthorized("Authentication information is incomplete");
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                var prescriptions = await repository.GetNewPrescriptionsAsync(page, pageSize, rezeptType);
                var prescriptionDtos = prescriptions.ToList();

                var totalCount = await repository.GetNewPrescriptionsCountAsync(rezeptType);
                
                var response = new PrescriptionResponse
                {
                    Prescriptions = prescriptionDtos,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    HasNextPage = (page * pageSize) < totalCount
                };

                _logger.LogInformation("Retrieved {Count} new prescriptions for client {ClientCode} with filter {RezeptType}", 
                    prescriptionDtos.Count, clientCode, rezeptType ?? "all");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving new prescriptions");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("mark-as-read")]
        public async Task<ActionResult<BulkOperationResponse>> MarkAsRead(
            [FromBody] BulkOperationRequest request)
        {
            try
            {
                var connectionString = User.FindFirst("ConnectionString")?.Value;
                var clientCode = User.FindFirst("ClientCode")?.Value;
                
                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(clientCode))
                {
                    _logger.LogWarning("Unauthorized access attempt - missing connection string or client code");
                    return Unauthorized("Authentication information is incomplete");
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                var prescriptions = request.Prescriptions.Select(p => new PrescriptionDto
                {
                    Type = p.Type,
                    RezeptUuid = p.RezeptUuid,
                    Id = 0, // Not used when working with UUIDs
                    XmlRequest = null,
                    TransferArz = false,
                    RzLieferId = null,
                    Muster16Id = null,
                    TransaktionsNummer = null,
                    ErezeptId = null,
                    DatenId = null
                });

                await repository.MarkAsReadByUuidAsync(prescriptions);

                _logger.LogInformation("Marked {Count} prescriptions as read by UUID for client {ClientCode}", 
                    request.Prescriptions.Count(), clientCode);

                return Ok(new BulkOperationResponse
                {
                    Success = true,
                    ProcessedCount = request.Prescriptions.Count(),
                    Errors = new List<string>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking prescriptions as read by UUID");
                return StatusCode(500, new BulkOperationResponse
                {
                    Success = false,
                    ProcessedCount = 0,
                    Errors = new[] { "Internal server error" }
                });
            }
        }

        [HttpPost("set-abgerechnet")]
        public async Task<ActionResult<BulkOperationResponse>> SetStatusAbgerechnet(
            [FromBody] BulkOperationRequest request)
        {
            try
            {
                var connectionString = User.FindFirst("ConnectionString")?.Value;
                var clientCode = User.FindFirst("ClientCode")?.Value;
                
                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(clientCode))
                {
                    _logger.LogWarning("Unauthorized access attempt - missing connection string or client code");
                    return Unauthorized("Authentication information is incomplete");
                }

                using var context = _dbContextFactory.CreateDbContext(connectionString);
                var repository = new PrescriptionRepository(context);
                
                var prescriptions = request.Prescriptions.Select(p => new PrescriptionDto
                {
                    Type = p.Type,
                    RezeptUuid = p.RezeptUuid,
                    Id = 0, // Not used when working with UUIDs
                    XmlRequest = null,
                    TransferArz = false,
                    RzLieferId = null,
                    Muster16Id = null,
                    TransaktionsNummer = null,
                    ErezeptId = null,
                    DatenId = null
                });

                await repository.SetStatusAbgerechnetByUuidAsync(prescriptions);

                _logger.LogInformation("Set {Count} prescriptions to ABGERECHNET by UUID for client {ClientCode}", 
                    request.Prescriptions.Count(), clientCode);

                return Ok(new BulkOperationResponse
                {
                    Success = true,
                    ProcessedCount = request.Prescriptions.Count(),
                    Errors = new List<string>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting prescriptions to ABGERECHNET by UUID");
                return StatusCode(500, new BulkOperationResponse
                {
                    Success = false,
                    ProcessedCount = 0,
                    Errors = new[] { "Internal server error" }
                });
            }
        }
    }
}