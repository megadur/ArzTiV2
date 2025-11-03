using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ArzTi3Server.Services;

namespace ArzTi3Server.Middleware
{
    public class TenantConnectionMiddleware : IMiddleware
    {
        private readonly ITenantService _tenantService;

        public TenantConnectionMiddleware(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Extract tenant information from the request
            // You might get it from a header, subdomain, or other source
            string tenantId = context.Request.Headers["X-Tenant-Id"].ToString();

            if (!string.IsNullOrEmpty(tenantId))
            {
                // Set the current tenant for this request
                _tenantService.SetCurrentTenant(tenantId);
            }

            // Continue processing the request
            await next(context);
        }
    }
}