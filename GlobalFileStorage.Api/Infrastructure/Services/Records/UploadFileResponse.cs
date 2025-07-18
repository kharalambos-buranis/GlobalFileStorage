namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UploadFileResponse(
       Guid FileId,
       string PresignedUploadUrl,
       DateTime Expiration
    );
    
    
}
