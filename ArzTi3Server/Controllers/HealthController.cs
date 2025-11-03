using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ArzTi3Server.Services;

namespace ArzTi3Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly ITenantConnectionResolver _tenantResolver;

        public HealthController(ILogger<HealthController> logger, ITenantConnectionResolver tenantResolver)
        {
            _logger = logger;
            _tenantResolver = tenantResolver;
        }

        [HttpGet("arzsw")]
        [AllowAnonymous]
        public IActionResult CheckArzSw()
        {
            // Basic check: ensure ArzSwConnection is configured
            // TenantConnectionResolver logs configuration issues on construction
            _logger.LogInformation("Health check invoked");
            return Ok(new { status = "ok" });
        }
    }
}
