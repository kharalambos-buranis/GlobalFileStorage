using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Infrastructure.Services.Records;

namespace GlobalFileStorage.Api.Domain.ServiceLayerInterfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(RegisterUserRequest request);
        Task<User?> GetByEmailAsync(string email);
        Task<LoginResponse> LoginAsync(LoginRequest request);

    }
}
