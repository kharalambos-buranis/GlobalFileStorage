using GlobalFileStorage.Api.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorage.Api.Infrastructure.Configurations
{
    public class UserGonfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.UserId).HasName("pk_users");

            builder.Property(u => u.UserId).HasColumnName("id");
            builder.Property(u => u.Tenant).HasColumnName("tenant");
            builder.Property(u => u.Email).HasColumnName("email");
            builder.Property(u => u.Role).HasColumnName("role");
            builder.Property(u => u.PermissionsJson).HasColumnName("permission_json");
            builder.Property(u => u.LastLoginTimestamp).HasColumnName("last_login_timestamp");
            builder.Property(u => u.MFAEnabled).HasColumnName("MFA_enabled");
            builder.Property(u => u.APIKeyHash).HasColumnName("API_key_hash");
            builder.Property(u => u.SessionTimeout).HasColumnName("session_timeout");
            builder.Property(u => u.IPWhitelist).HasColumnName("IP_whitelist");
        }
    }
}
