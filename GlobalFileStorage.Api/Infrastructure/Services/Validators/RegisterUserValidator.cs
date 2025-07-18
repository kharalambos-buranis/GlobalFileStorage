using FluentValidation;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Must contain at least one uppercase.")
                .Matches("[a-z]").WithMessage("Must contain at least one lowercase.")
                .Matches("[0-9]").WithMessage("Must contain at least one number.");

            RuleFor(x => x.Role)
                .IsInEnum();

            RuleFor(x => x.TenantId)
                .NotEmpty();
        }
    }
}
