using GlobalFileStorage.Api.Common.Entities;

namespace GlobalFileStorage.Api.Domain.RepositoryInterfaces
{
    public interface IFileItemRepository : IBaseRepository<FileItem>
    {
        Task<List<FileItem>> GetByTenantAsync(Guid tenantId);
        Task<List<FileItem>> GetByUserAsync(Guid userId);
        Task<List<FileItem>> GetByTagsAsync(Guid tenantId, List<string> tags);
    }
}
