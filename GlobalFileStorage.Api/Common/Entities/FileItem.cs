using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Common.Entities
{
    public class FileItem
    {
        public Guid FileId { get; set; } 
        public Guid TenantId { get; set; }
        public Guid UserId { get;  set; }
        public Tenant Tenant { get; set; } = default!;
        public User User { get; set; } = default!;

        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; } 
        public string ContentType { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty;

        public string MD5Hash { get; set; } = string.Empty;
        public string SHA256Hash { get; set; } = string.Empty;
        public Guid EncryptionKeyId { get; set; }

        public DateTime UploadTimestamp { get; set; }
        public DateTime? LastAccessedTimestamp { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public int VersionNumber { get; set; } = 1;
        public Dictionary<string, string> Metadata { get; set; } = new();
        public List<string> Tags { get; set; } = new();

        public AccessLevel AccessLevel { get; set; }
    }
}
