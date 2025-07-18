using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;

public interface ITenantService
{
    Task<TenantResponse> RegisterTenantAsync(RegisterTenantRequest request);
    Task<TenantResponse?> GetBySubdomainAsync(string subdomain);
}
