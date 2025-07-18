namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces
{
    public interface IFileStorageService
    {
        Task<string> GenerateUploadUrlAsync(string bucketName, string objectName, TimeSpan expiration);
        Task<string> GenerateDownloadUrlAsync(string bucketName, string objectName, TimeSpan expiration);
        Task DeleteFileAsync(string bucketName, string objectName); 
    }
}
