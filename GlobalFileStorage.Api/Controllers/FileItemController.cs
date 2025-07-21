using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Records;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileItemController : ControllerBase
    {
        private readonly IFileItemService _fileItemService;

        public FileItemController(IFileItemService fileItemService)
        {
            _fileItemService = fileItemService;
        }

        [HttpPost]

        public async Task<IActionResult> RegisterFileItemAsync([FromBody] UploadFileRequest request)
        {
            var result = await _fileItemService.RegisterUploadAsync(request);

            return Ok(result);
        }

        [HttpGet("tenantid")]

        public async Task<IActionResult> GetFileItemsByIdAsync([FromRoute] Guid id)
        {
            var result = await _fileItemService.GetFilesByTenantAsync(id);
            return Ok(result);
        }

        [HttpGet("tag")]

        public async Task<IActionResult> GetFileItemsByTagAync([FromQuery]Guid tenantId,[FromQuery] List<string> tags)
        {
            var result = await _fileItemService.GetFilesByTagsAsync(tenantId, tags);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(Guid fileId, Guid tenantId)
        {
            await _fileItemService.DeleteFileAsync(fileId, tenantId);
            return NoContent();
        }
    }
}
