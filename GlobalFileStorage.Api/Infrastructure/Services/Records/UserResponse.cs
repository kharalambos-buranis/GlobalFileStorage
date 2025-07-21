namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UserResponse(
      Guid UserId,
      string Email,
      string Role,
      Guid? TenantId
    );
    

    
}
