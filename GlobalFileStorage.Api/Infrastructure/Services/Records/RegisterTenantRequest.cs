using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record RegisterTenantRequest
    (
        string OrganizationName,
        string SubdomainPrefix,
        string Region,
        BillingPlan BillingPlan,
        EncryptionRequirement EncryptionRequirement,
        ComplianceFlags ComplianceFlags
    );
}
