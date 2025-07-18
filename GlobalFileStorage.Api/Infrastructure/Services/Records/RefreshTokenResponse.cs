namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record RefreshTokenResponse(
       string NewAccessToken,
       DateTime ExpiresAt 
    );
    
    
}
