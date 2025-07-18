using GlobalFileStorage.Api.Common.Entities;

namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces
{
    public interface IUsageStatsService
    {
        Task<UsageStats?> GetByTenantIdAsync(Guid tenantId);
        Task UpdateStorageUsageAsync(Guid tenantId, long bytesDelta);
        Task UpdateBandwidthUsageAsync(Guid tenantId, long bytesDelta);
        Task IncrementApiCallsAsync(Guid tenantId);
        Task ResetDailyQuotaAsync(Guid tenantId);
        
    }
}
