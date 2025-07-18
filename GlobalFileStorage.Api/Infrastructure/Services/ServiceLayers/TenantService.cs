using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Common.Enums;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Exceptions;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers;

public class TenantService : ITenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<TenantResponse> RegisterTenantAsync(RegisterTenantRequest request)
    {
        var existing = await _tenantRepository.GetBySubdomainAsync(request.SubdomainPrefix);
       
        if (existing is not null)
        {
            throw new ConflictException("Subdomain is already in use.");
        }

        var tenant = new Tenant
        {
            TenantId = Guid.NewGuid(),
            OrganizationName = request.OrganizationName,
            SubdomainPrefix = request.SubdomainPrefix,
            TenantStatus = TenantStatus.PendingActivation,
            BillingPlan = request.BillingPlan,
            StorageQuota = 10_000_000_000, 
            BandwidthQuota = 1_000_000_000,
            APIRateLimit = 1000,
            DataResidencyRegion = request.Region,
            ComplianceRequirements = request.ComplianceFlags,
            EncryptionRequirements = request.EncryptionRequirement
        };

        await _tenantRepository.AddAsync(tenant);
        await _tenantRepository.SaveChangesAsync();

        return new TenantResponse(
            tenant.TenantId,
            tenant.OrganizationName,
            tenant.SubdomainPrefix,
            tenant.BillingPlan,
            tenant.TenantStatus
        );
    }

    public async Task<TenantResponse?> GetBySubdomainAsync(string subdomain)
    {
        var tenant = await _tenantRepository.GetBySubdomainAsync(subdomain);
        if (tenant is null) return null;

        return new TenantResponse(
            tenant.TenantId,
            tenant.OrganizationName,
            tenant.SubdomainPrefix,
            tenant.BillingPlan,
            tenant.TenantStatus
        );
    }
}