using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers
{
    public class UsageStatsService : IUsageStatsService 
    {
        private readonly IUsageStatsRepository _usageStatsRepository;

        public UsageStatsService(IUsageStatsRepository usageStatsRepository)
        {
            _usageStatsRepository = usageStatsRepository;
        }

        public async Task<List<UsageResponse>> GetByTenantIdAsync(Guid tenantId)
        {
            return (await _usageStatsRepository
                .GetByTenantIdAsync(tenantId))
                .Select(x => new UsageResponse(x.TenantId, x.StorageUsed, x.BandwidthUsed, x.APICallsCount, x.FileOperationCount))
                .ToList();

        }

        public async Task UpdateStorageUsageAsync(Guid tenantId, long bytesDelta)
        {
            var stats = await _usageStatsRepository.GetByIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.StorageUsed += bytesDelta;

            if (stats.StorageUsed < 0)
                stats.StorageUsed = 0;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task UpdateBandwidthUsageAsync(Guid tenantId, long bytesDelta)
        {
            var stats = await _usageStatsRepository.GetByIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.BandwidthUsed += bytesDelta;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task IncrementApiCallsAsync(Guid tenantId)
        {
            var stats = await _usageStatsRepository.GetByIdAsync(tenantId)
                        ?? new UsageStats { TenantId = tenantId };

            stats.APICallsCount += 1;

            await _usageStatsRepository.SaveChangesAsync();
        }

        public async Task ResetDailyQuotaAsync(Guid tenantId)
        {
            var stats = await _usageStatsRepository.GetByIdAsync(tenantId);
            if (stats is null) return;

            stats.APICallsCount = 0;
            stats.BandwidthUsed = 0;

            await _usageStatsRepository.SaveChangesAsync();
        }
    }
}
