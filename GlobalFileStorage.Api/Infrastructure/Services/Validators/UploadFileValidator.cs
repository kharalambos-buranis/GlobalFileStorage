using FluentValidation;
using GlobalFileStorage.Api.Infrastructure.Services.Records;
using System.Text.RegularExpressions;

namespace GlobalFileStorage.Api.Infrastructure.Services.Validators
{
    public class UploadFileValidator : AbstractValidator<UploadFileRequest>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty()
                .MaximumLength(255)
                .Matches(@"^[^\\/:*?""<>|]+$")
                .WithMessage("File name contains invalid characters.");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("ContentType (MIME) is required.");

            RuleFor(x => x.FileSize)
                .GreaterThan(0)
                .WithMessage("File size must be greater than 0.")
                .LessThanOrEqualTo(10L * 1024 * 1024 * 1024 * 1024) 
                .WithMessage("File size exceeds maximum limit (10TB).");

            RuleFor(x => x.Metadata)
                .Must(m => m == null || m.Count <= 50)
                .WithMessage("You can only include up to 50 metadata pairs.");

            RuleForEach(x => x.Metadata)
                .Must(pair => Regex.IsMatch(pair.Key, @"^[a-zA-Z0-9_]+$"))
                .WithMessage("Metadata keys must be alphanumeric with underscores only.");

            RuleFor(x => x.Tags)
                .Must(t => t == null || t.Count <= 20)
                .WithMessage("You can only include up to 20 tags.");
        }
    }
}
