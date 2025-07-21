using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Common.Enums;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Exceptions;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers
{
    public class FileItemService : IFileItemService
    {
        private readonly IFileItemRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;

        public FileItemService(IFileItemRepository fileRepository, IFileStorageService fileStorageService) 
        {
            _fileRepository = fileRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<UploadFileResponse> RegisterUploadAsync(UploadFileRequest request)
        {
            var fileId = Guid.NewGuid();
            var objectPath = $"{request.tenantId}/{fileId}";

            var fileItem = new FileItem
            {
                FileId = fileId,
                FileName = request.FileName,
                FileSize = request.FileSize,
                ContentType = request.ContentType,
                StoragePath = objectPath,
                TenantId = request.tenantId,
                UserId = request.userId,
                UploadTimestamp = DateTime.UtcNow,
                AccessLevel = AccessLevel.Private,
                Metadata = request.Metadata ?? new Dictionary<string, string>(),
                Tags = request.Tags ?? new List<string>(),
                VersionNumber = 1
            };

            await _fileRepository.AddAsync(fileItem);
            await _fileRepository.SaveChangesAsync();

            var uploadUrl = await _fileStorageService.GenerateUploadUrlAsync(request.tenantId.ToString(), objectPath, TimeSpan.FromMinutes(10));

            return new UploadFileResponse(
                fileItem.FileId,
                uploadUrl,
                DateTime.UtcNow.AddMinutes(10)
            );
        }

        public async Task<List<FileMetadataResponse>> GetFilesByTenantAsync(Guid tenantId)
        { 
            var files = await _fileRepository.GetByTenantAsync(tenantId);
            return files.Select(ToMetadataResponse).ToList();
        }

        public async Task<List<FileMetadataResponse>> GetFilesByTagsAsync(Guid tenantId, List<string> tags)
        {
            var files = await _fileRepository.GetByTagsAsync(tenantId, tags);
            return files.Select(ToMetadataResponse).ToList();
        }

        public async Task DeleteFileAsync(Guid fileId, Guid tenantId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file is null || file.TenantId != tenantId)
            {
                throw new NotFoundException("File not found or unauthorized.");
            }

            await _fileStorageService.DeleteFileAsync(tenantId.ToString(), file.StoragePath);
            _fileRepository.Delete(file);
            await _fileRepository.SaveChangesAsync();
        }

        private static FileMetadataResponse ToMetadataResponse(FileItem file) =>
            new(
                file.FileId,
                file.FileName,
                file.ContentType,
                file.FileSize,
                file.UploadTimestamp,
                file.Tags,
                file.Metadata,
                file.AccessLevel
            );
    }
}
