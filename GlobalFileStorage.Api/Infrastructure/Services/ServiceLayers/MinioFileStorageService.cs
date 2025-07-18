using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using Minio;
using Minio.DataModel.Args;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers;

public class MinioFileStorageService : IFileStorageService
{

    private readonly IMinioClient _minioClient;

    public MinioFileStorageService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> GenerateUploadUrlAsync(string bucketName, string objectName, TimeSpan expiration)
    {
        var args = new PresignedPutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry((int)expiration.TotalSeconds);

        return await _minioClient.PresignedPutObjectAsync(args);
    }

    public async Task<string> GenerateDownloadUrlAsync(string bucketName, string objectName, TimeSpan expiration)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry((int)expiration.TotalSeconds);

        return await _minioClient.PresignedGetObjectAsync(args);
    }

    public async Task DeleteFileAsync(string bucketName, string objectName)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        await _minioClient.RemoveObjectAsync(args);
    }
}
