using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GlobalFileStorage.Api.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly TenantDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TenantDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _dbSet.FindAsync(id).AsTask();
        }

        public Task AddAsync(T entity)
        {
            return _dbSet.AddAsync(entity).AsTask();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
