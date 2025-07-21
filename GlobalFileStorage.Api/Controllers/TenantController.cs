using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Records;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateTenant([FromBody] RegisterTenantRequest request)
        {
            var result = await _tenantService.RegisterTenantAsync(request);

            return Ok(result);
           // return CreatedAtAction(nameof(GetByIdAsync), new { id = result.TenantId }, result);
        }

        [HttpGet("{subdomain}")]

        public async Task<IActionResult> GetTenantBySubdomain(string subdomain)
        {
            var result = await _tenantService.GetBySubdomainAsync(subdomain);

            return Ok(result);
        }
    }
}
