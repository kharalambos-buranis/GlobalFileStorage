using GlobalFileStorage.Api.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorage.Api.Infrastructure.Configurations
{
    public class UsageStatsConfiguration : IEntityTypeConfiguration<UsageStats>
    {
        public void Configure(EntityTypeBuilder<UsageStats> builder)
        {
            builder.ToTable(nameof(UsageStats));

            builder.HasKey(u => u.TenantId);

            builder.Property(u => u.TenantId).HasColumnName("tenant_id");
            builder.Property(u => u.StorageUsed).HasColumnName("storage_used");
            builder.Property(u => u.BandwidthUsed).HasColumnName("bandwidth_used");
            builder.Property(u => u.APICallsCount).HasColumnName("API_calss_count");
            builder.Property(u => u.FileOperationCount).HasColumnName("file_operation_count");
            builder.Property(u => u.ActiveUserCount).HasColumnName("active_user_count");
        }
    }
}
