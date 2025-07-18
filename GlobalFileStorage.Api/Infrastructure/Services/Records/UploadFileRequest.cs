namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UploadFileRequest(
       string FileName,
       string ContentType,
       long FileSize,
       Dictionary<string, string> Metadata,
       List<string> Tags
    );
    
    
}
