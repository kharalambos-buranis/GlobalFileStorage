using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorage.Api.Infrastructure.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(TenantDbContext context) : base(context)
        {
        }

        public async Task<Tenant?> GetBySubdomainAsync(string subdomain)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.SubdomainPrefix == subdomain);
        }
    }
}
