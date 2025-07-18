namespace GlobalFileStorage.Api.Common.Entities;

public class UsageStats
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = default!;

    public long StorageUsed { get; set; }
    public long BandwidthUsed { get; set; }
    public int APICallsCount { get; set; }
    public int FileOperationCount { get; set; }
    public int ActiveUserCount { get; set; }
}

