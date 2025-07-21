using GlobalFileStorage.Api.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorage.Api.Infrastructure.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable(nameof(Tenant));

        builder.HasKey(t => t.TenantId).HasName("pk_tenants");

        builder.Property(t => t.TenantId).HasColumnName("id");
        builder.Property(t => t.OrganizationName).HasColumnName("organization_name");
        builder.Property(t => t.SubdomainPrefix).HasColumnName("subdomain_prefix");
        builder.Property(t => t.TenantStatus).HasColumnName("tenant_status");
        builder.Property(t => t.BillingPlan).HasColumnName("billing_plan");
        builder.Property(t => t.StorageQuota).HasColumnName("storage_quota");
        builder.Property(t => t.BandwidthQuota).HasColumnName("bandwidth_quota");
        builder.Property(t => t.APIRateLimit).HasColumnName("API_rate_limit");
        builder.Property(t => t.DataResidencyRegion).HasColumnName("data_residency_region");
        builder.Property(t => t.ComplianceRequirements).HasColumnName("comliance_requirements");
        builder.Property(t => t.EncryptionRequirements).HasColumnName("encryption_requirements");

        builder
          .HasMany(t => t.Users)
          .WithOne(u => u.Tenant)
          .HasForeignKey(u => u.TenantId);

        builder
          .HasMany(t => t.Files)
          .WithOne(f => f.Tenant)
          .HasForeignKey(f => f.TenantId);

     //    builder
     //.HasOne(t => t.UsageStats)
     //.WithOne(u => u.Tenant)
     //.HasForeignKey<UsageStats>(u => u.TenantId);
    }
}
