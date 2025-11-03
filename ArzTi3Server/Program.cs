using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Authentication;
using ArzTi3Server.Services;
using Microsoft.OpenApi.Models;
using ArzTi3Server.Domain.Model.ArzSw;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ArzTi3Server.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ArzTi3Server.Health;

namespace ArzTi3Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add API Versioning for ARZ_TI 2.0
            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(2, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-API-Version"));
            }).AddMvc().AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            // Add DbContext for ArzSw (multitenancy management)
            builder.Services.AddDbContext<ArzSwDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ArzSwConnection")));

            // Add multitenancy services
            builder.Services.AddScoped<IMultitenantDbContextFactory, MultitenantDbContextFactory>();

            // Memory cache for tenant connection strings
            builder.Services.AddMemoryCache();

            // Tenant resolver service
            builder.Services.AddSingleton<ITenantConnectionResolver, TenantConnectionResolver>();

            // Add tenant services - must register ITenantService before TenantConnectionMiddleware
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<TenantConnectionMiddleware>();

            // Add Basic Authentication
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            builder.Services.AddAuthorization();

            // Configure Swagger with API Versioning and Basic Authentication
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Configure Swagger for v1 (Legacy)
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ArzTi3 Prescription Mediation API",
                    Version = "v1",
                    Description = "Legacy API for ARZ prescription insurance mediation system - Will be deprecated"
                });

                // Configure Swagger for v2 (ARZ_TI 2.0)
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "ARZ_TI 2.0 API",
                    Version = "v2",
                    Description = "High-performance ARZ prescription mediation API with enhanced features and bulk operations"
                });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Add Health Checks
            builder.Services.AddHealthChecks()
                .AddCheck<ArzSwDatabaseHealthCheck>("arz_sw_database_health", 
                    tags: new[] { "database", "management", "ready" })
                .AddCheck<TenantDatabaseHealthCheck>("tenant_database_health", 
                    tags: new[] { "database", "tenant", "ready" })
                .AddCheck<SystemResourceHealthCheck>("system_resources", 
                    tags: new[] { "memory", "system", "ready" });

            // Add system monitoring
            builder.Services.AddHostedService<Services.SystemMonitoringService>();
            builder.Services.Configure<Services.SystemMonitoringOptions>(options => 
            {
                options.MonitoringIntervalSeconds = 60;
                options.MemoryThresholdBytes = 1024 * 1024 * 1024; // 1GB
                options.MonitorCpu = true;
                options.MonitorMemory = true;
                options.LogFilePath = "system_monitoring.log";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "ARZ_TI 2.0 API v2");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArzTi3 Prescription Mediation API v1");
                    c.DefaultModelsExpandDepth(-1); // Hide schemas section
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Use the middleware with the fully qualified name to avoid namespace confusion
            app.UseMiddleware<TenantConnectionMiddleware>();

            // Custom response writer for health checks
            static Task WriteResponse(HttpContext context, HealthReport result)
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = result.Status.ToString(),
                    checks = result.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        duration = e.Value.Duration
                    })
                };
                return context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }

            // Map health endpoints
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = WriteResponse
            });
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = WriteResponse
            });
            app.MapHealthChecks("/health/system", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("memory"),
                ResponseWriter = WriteResponse
            });

            app.Run();
        }
    }
}
