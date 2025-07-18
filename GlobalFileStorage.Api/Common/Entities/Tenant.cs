using GlobalFileStorage.Api.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalFileStorage.Api.Common.Entities
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string SubdomainPrefix { get; set; } = string.Empty;
        public TenantStatus TenantStatus { get; set; }
        public BillingPlan BillingPlan { get; set; }

        public long StorageQuota { get; set; }  
        public long BandwidthQuota { get; set; }  
        public int APIRateLimit { get; set; }

        public string DataResidencyRegion { get; set; } = string.Empty;
        public ComplianceFlags ComplianceRequirements { get; set; }
        public EncryptionRequirement EncryptionRequirements { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<FileItem> Files { get; set; } = new List<FileItem>();
    }
}
