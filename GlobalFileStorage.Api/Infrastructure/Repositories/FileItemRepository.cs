using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorage.Api.Infrastructure.Repositories
{
    public class FileItemRepository : BaseRepository<FileItem>, IFileItemRepository
    {
        public FileItemRepository(TenantDbContext context) : base(context)
        {
        }

        public async Task<List<FileItem>> GetByTagsAsync(Guid tenantId, List<string> tags)
        {
            return await _dbSet
            .Where(f => f.TenantId == tenantId && f.Tags.Any(tag => tags.Contains(tag)))
            .ToListAsync();
        }

        public async Task<List<FileItem>> GetByTenantAsync(Guid tenantId)
        {
            return await _dbSet
            .Where(f => f.TenantId == tenantId)
            .OrderByDescending(f => f.UploadTimestamp)
            .ToListAsync();

        }

        public async Task<List<FileItem>> GetByUserAsync(Guid userId)
        {
            return await _dbSet
            .Where(f => f.UserId == userId) 
            .OrderByDescending(f => f.UploadTimestamp)
            .ToListAsync();
        }
    }
}
