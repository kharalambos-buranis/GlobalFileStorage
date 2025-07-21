using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces
{
    public interface IUsageStatsService
    {
        Task<List<UsageResponse>> GetByTenantIdAsync(Guid tenantId);
        Task UpdateStorageUsageAsync(Guid tenantId, long bytesDelta);
        Task UpdateBandwidthUsageAsync(Guid tenantId, long bytesDelta);
        Task IncrementApiCallsAsync(Guid tenantId);
        Task ResetDailyQuotaAsync(Guid tenantId);
        
    }
}
