using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;

namespace GlobalFileStorage.Api.Infrastructure.Repositories
{
    public class UsageStatsReposiotry : BaseRepository<UsageStats>, IUsageStatsRepository
    {
        public UsageStatsReposiotry(TenantDbContext context) : base(context)
        {
        }
    }
}
