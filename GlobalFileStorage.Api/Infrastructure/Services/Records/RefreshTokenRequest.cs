namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record RefreshTokenRequest(
      string AccessToken, string RefreshToken
    );
    
    
}
