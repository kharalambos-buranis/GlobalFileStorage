using GlobalFileStorage.Api.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalFileStorage.Api.Infrastructure.Configurations
{
    public class FileItemConfiguration : IEntityTypeConfiguration<FileItem>
    {
        public void Configure(EntityTypeBuilder<FileItem> builder)
        {
            builder.ToTable(nameof(FileItem));

            builder.Property(f => f.FileId).HasColumnName("id");
            builder.Property(f => f.Tenant).HasColumnName("tenant");
            builder.Property(f => f.FileName).HasColumnName("file_name");
            builder.Property(f => f.FileSize).HasColumnName("file_size");
            builder.Property(f => f.ContentType).HasColumnName("content_type");
            builder.Property(f => f.StoragePath).HasColumnName("storage_path");
            builder.Property(f => f.MD5Hash).HasColumnName("MD5_hash");
            builder.Property(f => f.SHA256Hash).HasColumnName("SHA256_hash");
            builder.Property(f => f.EncryptionKeyId).HasColumnName("encryption_key_id");
            builder.Property(f => f.UploadTimestamp).HasColumnName("upload_timestamp");
            builder.Property(f => f.LastAccessedTimestamp).HasColumnName("last_accessed_timestamp");
            builder.Property(f => f.ExpirationDate).HasColumnName("expiration_date");
            builder.Property(f => f.VersionNumber).HasColumnName("version_number");
            builder.Property(f => f.Metadata).HasColumnName("metadata");
            builder.Property(f => f.Tags).HasColumnName("tags"); 
            builder.Property(f => f.AccessLevel).HasColumnName("access_level"); 
        }
    }
}
