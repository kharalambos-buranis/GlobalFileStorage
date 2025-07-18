using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Common.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = default!;

        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string PermissionsJson { get; set; } = "{}";

        public DateTime LastLoginTimestamp { get; set; }
        public bool MFAEnabled { get; set; }

        public string APIKeyHash { get; set; } = string.Empty;
        public TimeSpan SessionTimeout { get; set; }
        public string? IPWhitelist { get; set; } 
        public FileItem FileItem { get; set; }
    }
}
