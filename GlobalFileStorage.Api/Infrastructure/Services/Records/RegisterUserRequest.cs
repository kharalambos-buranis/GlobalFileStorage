using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record RegisterUserRequest(
        string Email,
        string Password,
        UserRole Role,
        Guid TenantId
    );
    
    
}
