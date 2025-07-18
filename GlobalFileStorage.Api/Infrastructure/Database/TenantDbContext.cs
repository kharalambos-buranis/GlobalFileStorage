using GlobalFileStorage.Api.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GlobalFileStorage.Api.Infrastructure.Database
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsageStats> UsageStats { get; set; }
        public DbSet<FileItem> FileItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
