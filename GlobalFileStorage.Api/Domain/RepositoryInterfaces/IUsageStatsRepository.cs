using GlobalFileStorage.Api.Common.Entities;

namespace GlobalFileStorage.Api.Domain.RepositoryInterfaces;

public interface IUsageStatsRepository : IBaseRepository<UsageStats>
{
    public Task<UsageStats?> GetByTenantIdAsync(Guid tenantId);
}
