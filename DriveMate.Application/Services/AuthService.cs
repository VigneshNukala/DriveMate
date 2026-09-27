using DriveMate.Application.DTOs.Authentication;
using DriveMate.Application.Interfaces.IRepositories;
using DriveMate.Application.Interfaces.Services;
using DriveMate.Domain.Models.Users;

namespace DriveMate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {

        var email = request.Email.Trim().ToLowerInvariant();
        // 1. Check if email already exists
        var existingUser = await _userRepository.GetByEmailAsync(email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        // 2. Hash password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            PhoneNumber = request.PhoneNumber?.Trim(),
            PasswordHash = passwordHash,
            Role = "User",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // 4. Generate JWT
        var token = _jwtTokenService.GenerateToken(user);

        // 5. Save to database
        var createdUser = await _userRepository.CreateAsync(user);

        

        // 6. Return response
        return new AuthResponse
        {
            UserId = createdUser.Id,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Email = createdUser.Email,
            Role = createdUser.Role,
            Token = token
        };
    }

    public async Task<AuthResponse> LoginAsync(
        LoginRequest request)
    {
        // 1. Find user
        var user =
            await _userRepository.GetByEmailAsync(
                request.Email.Trim().ToLowerInvariant());

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 2. Check active status
        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "This account is inactive.");
        }

        // 3. Verify password
        var passwordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 4. Generate JWT
        var token =
            _jwtTokenService.GenerateToken(user);

        // 5. Return response
        return new AuthResponse
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };
    }
}