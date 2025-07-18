namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UsageResponse(
      long StorageUsed,
      long BandwidthUsed,
      int APICallsThisMonth,
      int FileCount,
      long StorageQuota,
      long BandwidthQuota 
    );
   
    
}
