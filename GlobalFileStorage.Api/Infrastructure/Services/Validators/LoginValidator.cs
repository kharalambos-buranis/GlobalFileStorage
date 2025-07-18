using FluentValidation;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
