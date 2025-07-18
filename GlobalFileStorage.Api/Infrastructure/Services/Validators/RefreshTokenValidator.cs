using FluentValidation;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty();

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        } 
    }
}
