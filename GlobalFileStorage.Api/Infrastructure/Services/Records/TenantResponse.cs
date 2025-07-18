using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record TenantResponse(
      Guid TenantId,
      string OrganizationName,
      string SubdomainPrefix,
      BillingPlan BillingPlan,
      TenantStatus Status
    );


}
