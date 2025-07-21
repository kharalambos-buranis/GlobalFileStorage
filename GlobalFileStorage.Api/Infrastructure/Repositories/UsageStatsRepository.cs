using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorage.Api.Infrastructure.Repositories
{
    public class UsageStatsRepository : BaseRepository<UsageStats>, IUsageStatsRepository
    {
        public UsageStatsRepository(TenantDbContext context) : base(context)
        {
        }

        public async Task<List<UsageStats>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _dbSet
            .Where(f => f.TenantId == tenantId)
            .ToListAsync();

        }
    }
}
