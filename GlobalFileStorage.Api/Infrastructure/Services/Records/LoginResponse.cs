namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record LoginResponse(
       string AccessToken,
       string RefreshToken,
       DateTime ExpiresAt
    );
    

    
}
