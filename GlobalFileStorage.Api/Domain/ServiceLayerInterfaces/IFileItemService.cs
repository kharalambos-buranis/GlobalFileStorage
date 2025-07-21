using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces
{
    public interface IFileItemService
    {
        public Task<UploadFileResponse> RegisterUploadAsync(UploadFileRequest request);
        public Task<List<FileMetadataResponse>> GetFilesByTenantAsync(Guid tenantId);
        public Task<List<FileMetadataResponse>> GetFilesByTagsAsync(Guid tenantId, List<string> tags);
        public Task DeleteFileAsync(Guid fileId, Guid tenantId);

    }
}
