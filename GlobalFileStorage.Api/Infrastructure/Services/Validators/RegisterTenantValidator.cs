using FluentValidation;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.Validators
{
    public class RegisterTenantValidator : AbstractValidator<RegisterTenantRequest>
    {
        public RegisterTenantValidator()
        {
            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage("Organization name is required.")
                .MaximumLength(100);

            RuleFor(x => x.SubdomainPrefix)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9]")
                .MaximumLength(50)
                .WithMessage("Subdomain must be alphanumeric and 3–50 characters.");

            RuleFor(x => x.Region)
                .NotEmpty()
                .WithMessage("Region is required.");

            RuleFor(x => x.BillingPlan)
                .IsInEnum();

            RuleFor(x => x.EncryptionRequirement)
                .IsInEnum();

            RuleFor(x => x.ComplianceFlags)
                .NotNull();
        }
    }
}
