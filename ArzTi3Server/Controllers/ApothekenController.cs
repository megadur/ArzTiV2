using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ArzTi3Server.DTOs;
using ArzTi3Server.Services;
using ArzTi3Server.Domain.Repositories;
using ArzTi3Server.Domain.Model.ApoTi;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApothekenController : ControllerBase
    {
        private readonly ILogger<ApothekenController> _logger;
        private readonly IMultitenantDbContextFactory _dbContextFactory;
        private readonly IConfiguration _configuration;
        private readonly ITenantConnectionResolver _tenantResolver;

        public ApothekenController(
            ILogger<ApothekenController> logger,
            IMultitenantDbContextFactory dbContextFactory,
            IConfiguration configuration,
            ITenantConnectionResolver tenantResolver)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _configuration = configuration;
            _tenantResolver = tenantResolver;
        }

        [HttpGet]
        public async Task<ActionResult<ApothekeResponse>> GetApotheken(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100,
            [FromQuery] string? search = null)
        {
            try
            {
                var clientCode = User.FindFirst("ClientCode")?.Value;

                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user. Missing claim or mapping.");
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                IEnumerable<ErApotheke> apotheken;
                int totalCount;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    apotheken = await repository.SearchAsync(search, page, pageSize);
                    totalCount = await repository.GetSearchCountAsync(search);
                }
                else
                {
                    apotheken = await repository.GetAllAsync(page, pageSize);
                    totalCount = await repository.GetTotalCountAsync();
                }

                var apothekeDtos = apotheken.Select(MapToDto).ToList();

                var response = new ApothekeResponse
                {
                    Apotheken = apothekeDtos,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    HasNextPage = (page * pageSize) < totalCount
                };

                _logger.LogInformation("Retrieved {Count} apotheken for client {ClientCode}", 
                    apothekeDtos.Count, clientCode);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apotheken");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApothekeDto>> GetApothekeById(int id)
        {
            try
            {
                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user when getting by id {Id}", id);
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                var apotheke = await repository.GetByIdAsync(id);
                if (apotheke == null)
                {
                    return NotFound($"Apotheke with ID {id} not found");
                }

                return Ok(MapToDto(apotheke));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apotheke with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("by-ik/{ikNr:long}")]
        public async Task<ActionResult<ApothekeDto>> GetApothekeByIkNr(long ikNr)
        {
            try
            {
                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user when getting by IK {Ik}", ikNr);
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                var apotheke = await repository.GetByIkNrAsync(ikNr);
                if (apotheke == null)
                {
                    return NotFound($"Apotheke with IK-Nr {ikNr} not found");
                }

                return Ok(MapToDto(apotheke));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving apotheke with IK-Nr {IkNr}", ikNr);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApothekeDto>> CreateApotheke([FromBody] CreateApothekeRequest request)
        {
            try
            {
                var clientCode = User.FindFirst("ClientCode")?.Value;

                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user when creating apotheke");
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                // Check if IK-Nr already exists
                if (await repository.ExistsByIkNrAsync(request.ApoIkNr))
                {
                    return Conflict($"Apotheke with IK-Nr {request.ApoIkNr} already exists");
                }

                var apotheke = MapFromCreateRequest(request);
                var createdApotheke = await repository.CreateAsync(apotheke);

                _logger.LogInformation("Created apotheke {ApothekeName} (ID: {Id}) for client {ClientCode}", 
                    createdApotheke.ApothekeName, createdApotheke.IdApotheke, clientCode);

                return CreatedAtAction(
                    nameof(GetApothekeById),
                    new { id = createdApotheke.IdApotheke },
                    MapToDto(createdApotheke));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating apotheke");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApothekeDto>> UpdateApotheke(int id, [FromBody] UpdateApothekeRequest request)
        {
            try
            {
                var clientCode = User.FindFirst("ClientCode")?.Value;

                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user when updating apotheke {Id}", id);
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                var existingApotheke = await repository.GetByIdAsync(id);
                if (existingApotheke == null)
                {
                    return NotFound($"Apotheke with ID {id} not found");
                }

                // Check if IK-Nr already exists for a different apotheke
                if (await repository.ExistsByIkNrAsync(request.ApoIkNr, id))
                {
                    return Conflict($"Another apotheke with IK-Nr {request.ApoIkNr} already exists");
                }

                MapToExistingEntity(request, existingApotheke);
                var updatedApotheke = await repository.UpdateAsync(existingApotheke);

                _logger.LogInformation("Updated apotheke {ApothekeName} (ID: {Id}) for client {ClientCode}", 
                    updatedApotheke.ApothekeName, updatedApotheke.IdApotheke, clientCode);

                return Ok(MapToDto(updatedApotheke));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating apotheke with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteApotheke(int id)
        {
            try
            {
                var clientCode = User.FindFirst("ClientCode")?.Value;

                var tenantConnection = await _tenantResolver.ResolveForUserAsync(User);
                if (string.IsNullOrEmpty(tenantConnection))
                {
                    _logger.LogWarning("Could not resolve tenant connection for user when deleting apotheke {Id}", id);
                    return BadRequest("Tenant connection string not found or user claim missing");
                }

                using var context = _dbContextFactory.CreateDbContext(tenantConnection);
                var repository = new ApothekeRepository(context);

                var success = await repository.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Apotheke with ID {id} not found");
                }

                _logger.LogInformation("Deleted apotheke with ID {Id} for client {ClientCode}", 
                    id, clientCode);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting apotheke with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        private static ApothekeDto MapToDto(ErApotheke entity)
        {
            return new ApothekeDto
            {
                IdApotheke = entity.IdApotheke,
                ApothekeName = entity.ApothekeName,
                ApothekeNameZusatz = entity.ApothekeNameZusatz,
                ApoIkNr = entity.ApoIkNr,
                InhaberVorname = entity.InhaberVorname,
                InhaberNachname = entity.InhaberNachname,
                ApoIntNr = entity.ApoIntNr,
                Plz = entity.Plz,
                Ort = entity.Ort,
                Strasse = entity.Strasse,
                Email = entity.Email,
                Telefon = entity.Telefon,
                Mobil = entity.Mobil,
                Fax = entity.Fax,
                Bemerkung = entity.Bemerkung,
                Bundesland = entity.Bundesland,
                MandantType = entity.MandantType,
                IdLeType = entity.IdLeType,
                IdHauptapotheke = entity.IdHauptapotheke,
                IdHtAnrede = entity.IdHtAnrede,
                Filialapotheke = entity.Filialapotheke,
                Gesperrt = entity.Gesperrt,
                SecLogin = entity.SecLogin,
                SecLoginWerte = entity.SecLoginWerte,
                SecLoginNurApoUser = entity.SecLoginNurApoUser,
                AenIdSecUser = entity.AenIdSecUser,
                AenDatum = entity.AenDatum,
                AenZeit = entity.AenZeit
            };
        }

        private static ErApotheke MapFromCreateRequest(CreateApothekeRequest request)
        {
            return new ErApotheke
            {
                ApothekeName = request.ApothekeName,
                ApothekeNameZusatz = request.ApothekeNameZusatz,
                ApoIkNr = request.ApoIkNr,
                InhaberVorname = request.InhaberVorname,
                InhaberNachname = request.InhaberNachname,
                ApoIntNr = request.ApoIntNr,
                Plz = request.Plz,
                Ort = request.Ort,
                Strasse = request.Strasse,
                Email = request.Email,
                Telefon = request.Telefon,
                Mobil = request.Mobil,
                Fax = request.Fax,
                Bemerkung = request.Bemerkung,
                Bundesland = request.Bundesland,
                MandantType = request.MandantType,
                IdLeType = request.IdLeType,
                IdHauptapotheke = request.IdHauptapotheke,
                IdHtAnrede = request.IdHtAnrede,
                Filialapotheke = request.Filialapotheke,
                Gesperrt = request.Gesperrt,
                SecLogin = request.SecLogin,
                SecLoginWerte = request.SecLoginWerte,
                SecLoginNurApoUser = request.SecLoginNurApoUser
            };
        }

        private static void MapToExistingEntity(UpdateApothekeRequest request, ErApotheke entity)
        {
            entity.ApothekeName = request.ApothekeName;
            entity.ApothekeNameZusatz = request.ApothekeNameZusatz;
            entity.ApoIkNr = request.ApoIkNr;
            entity.InhaberVorname = request.InhaberVorname;
            entity.InhaberNachname = request.InhaberNachname;
            entity.ApoIntNr = request.ApoIntNr;
            entity.Plz = request.Plz;
            entity.Ort = request.Ort;
            entity.Strasse = request.Strasse;
            entity.Email = request.Email;
            entity.Telefon = request.Telefon;
            entity.Mobil = request.Mobil;
            entity.Fax = request.Fax;
            entity.Bemerkung = request.Bemerkung;
            entity.Bundesland = request.Bundesland;
            entity.MandantType = request.MandantType;
            entity.IdLeType = request.IdLeType;
            entity.IdHauptapotheke = request.IdHauptapotheke;
            entity.IdHtAnrede = request.IdHtAnrede;
            entity.Filialapotheke = request.Filialapotheke;
            entity.Gesperrt = request.Gesperrt;
            entity.SecLogin = request.SecLogin;
            entity.SecLoginWerte = request.SecLoginWerte;
            entity.SecLoginNurApoUser = request.SecLoginNurApoUser;
        }
    }
}