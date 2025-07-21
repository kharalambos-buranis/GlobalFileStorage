namespace GlobalFileStorage.Api.Infrastructure.Services.Records
{
    public record UploadFileRequest(
        Guid userId,
        Guid tenantId,
       string FileName,
       string ContentType,
       long FileSize,
       Dictionary<string, string> Metadata,
       List<string> Tags
    );
    
    
}
