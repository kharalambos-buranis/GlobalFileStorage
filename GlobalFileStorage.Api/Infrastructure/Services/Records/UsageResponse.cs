namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UsageResponse(
      Guid TenantId,
      long StorageUsed,
      long BandwidthUsed,
      int APICallsThisMonth,
      int FileCount
    );
   
    
}
