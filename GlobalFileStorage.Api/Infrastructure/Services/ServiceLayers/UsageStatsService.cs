using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers
{
    public class UsageStatsService : IUsageStatsService
    {
        private readonly IUsageStatsRepository _usageStatsRepository;

        public UsageStatsService(IUsageStatsRepository usageStatsRepository)
        {
            _usageStatsRepository = usageStatsRepository;
        }

        public async Task<UsageStats?> GetByTenantIdAsync(Guid tenantId)
        {
            return await _usageStatsRepository.GetByTenantIdAsync(tenantId);
        }

        public async Task UpdateStorageUsageAsync(Guid tenantId, long bytesDelta)
        {
            var stats = await _usageStatsRepository.GetByTenantIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.StorageUsed += bytesDelta;

            if (stats.StorageUsed < 0)
                stats.StorageUsed = 0;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task UpdateBandwidthUsageAsync(Guid tenantId, long bytesDelta)
        {
            var stats = await _usageStatsRepository.GetByTenantIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.BandwidthUsed += bytesDelta;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task IncrementApiCallsAsync(Guid tenantId)
        {
            var stats = await _usageStatsRepository.GetByTenantIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.APICallsMade += 1;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task ResetDailyQuotaAsync(Guid tenantId)
        {
            var stats = await _usageStatsRepository.GetByTenantIdAsync(tenantId);
            if (stats is null) return;

            stats.APICallsMade = 0;
            stats.BandwidthUsed = 0;

            await _usageStatsRepository.SaveChangesAsync();
        }
    }
}
