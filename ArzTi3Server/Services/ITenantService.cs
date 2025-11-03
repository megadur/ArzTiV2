namespace ArzTi3Server.Services;

public interface ITenantService
{
    void SetCurrentTenant(string tenantId);
}