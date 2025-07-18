namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record DownloadFileResponse(
      Guid FileId,
      string FileName,
      string PresignedDownloadUrl,
      DateTime Expiration
    );
    
}
