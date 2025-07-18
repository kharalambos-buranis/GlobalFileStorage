using GlobalFileStorage.Api.Common.Entities;
using GlobalFileStorage.Api.Common.Enums;
using GlobalFileStorage.Api.Domain.RepositoryInterfaces;
using GlobalFileStorage.Api.Domain.ServiceLayerInterfaces;
using GlobalFileStorage.Api.Infrastructure.Services.Exceptions;
using GlobalFileStorage.Api.Infrastructure.Services.Records;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GlobalFileStorage.Api.Infrastructure.Services.ServiceLayers;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly TokenProvider _tokenProvider;

    public UserService(
        IUserRepository userRepository,
        ITenantRepository tenantRepository,
        IPasswordHasher<User> passwordHasher,
        TokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public async Task<UserResponse> RegisterUserAsync(RegisterUserRequest request)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
       
        if (tenant == null || tenant.TenantStatus != TenantStatus.Active)
        {
            throw new InvalidOperationException("Invalid or inactive tenant.");
        }

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
       
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            TenantId = request.TenantId,
            Email = request.Email,
            Role = request.Role,
            MFAEnabled = false,
            SessionTimeout = TimeSpan.FromHours(2),
            APIKeyHash = string.Empty,
            LastLoginTimestamp = DateTime.MinValue
        };

        var passwordHash = _passwordHasher.HashPassword(user, request.Password);
        user.APIKeyHash = passwordHash;

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new UserResponse(user.UserId, user.Email, user.Role.ToString(), user.TenantId);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var existingEmail =  await _userRepository.GetByEmailAsync(email);

        if(existingEmail == null)
        {
            throw new NotFoundException("Email not found");
        }

        return existingEmail;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw new ConflictException("Email is not correct");
        } 

        var tenant = await _tenantRepository.GetByIdAsync(user.TenantId);
       
        if (tenant is null || tenant.TenantStatus != TenantStatus.Active)
        {
            throw new ConflictException("Tenant is inactive.");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.APIKeyHash, request.Password);
       
        if (result != PasswordVerificationResult.Success) 
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var accessToken = _tokenProvider.Create(user);
        var refreshToken = GenerateRefreshToken();

        return new LoginResponse(accessToken, refreshToken, DateTime.UtcNow.AddMinutes(15));
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }


}
