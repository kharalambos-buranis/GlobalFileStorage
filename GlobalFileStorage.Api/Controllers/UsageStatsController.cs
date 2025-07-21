using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageStatsController : ControllerBase
    {
        private readonly IUsageStatsService _usageStatsService;

        public UsageStatsController(IUsageStatsService usageStatsService)
        {
            _usageStatsService = usageStatsService;
        }

        [HttpGet("get-by-tenant-id")]
        public async Task<IActionResult> GetByTenantId(Guid tenantId)
        {
            var stats = await _usageStatsService.GetByTenantIdAsync(tenantId);
            return stats is not null ? Ok(stats) : NotFound();
        }

        [HttpPost("increment-api-call")]
        public async Task<IActionResult> IncrementApiCall(Guid tenantId)
        {
            await _usageStatsService.IncrementApiCallsAsync(tenantId);
            return Ok();
        }

        [HttpPost("update-storage")]
        public async Task<IActionResult> UpdateStorage(Guid tenantId, [FromQuery] long bytesDelta)
        {
            await _usageStatsService.UpdateStorageUsageAsync(tenantId, bytesDelta);
            return Ok();
        }

        [HttpPost("update-bandwidth")]
        public async Task<IActionResult> UpdateBandwidth(Guid tenantId, [FromQuery] long bytesDelta)
        {
            await _usageStatsService.UpdateBandwidthUsageAsync(tenantId, bytesDelta);
            return Ok();
        }

        [HttpPost("reset-daily-quota")]
        public async Task<IActionResult> ResetDailyQuota(Guid tenantId)
        {
            await _usageStatsService.ResetDailyQuotaAsync(tenantId);
            return Ok();
        }
    }
}
