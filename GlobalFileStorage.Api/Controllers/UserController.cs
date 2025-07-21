using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Records;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFileStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]

        public async Task<IActionResult> RegisterUserAync([FromBody] RegisterUserRequest request)
        {
            var result = await _userService.CreateUserAsync(request);

            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            return Ok(result);
        }

        [HttpGet("email")]

        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var result = await _userService.GetByEmailAsync(email);

            return Ok(result);
        }

    }
}
