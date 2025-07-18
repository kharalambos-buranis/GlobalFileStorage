using GlobalFileStorage.Api.Common.Entities;

namespace GlobalFileStorage.Api.Domain.RepositoryInterfaces
{
    public interface ITenantRepository : IBaseRepository<Tenant>
    {
        Task<Tenant?> GetBySubdomainAsync(string subdomain);
    }
}
