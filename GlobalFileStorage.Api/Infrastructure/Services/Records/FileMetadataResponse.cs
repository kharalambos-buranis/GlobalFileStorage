using GlobalFileStorage.Api.Common.Enums;

namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record FileMetadataResponse(
      Guid FileId,
      string FileName,
      string ContentType,
      long FileSize,
      DateTime UploadTimestamp,
      List<string> Tags,
      Dictionary<string, string> Metadata,
      AccessLevel AccessLevel
    );
    

    
}
