using DriveMate.Application.DTOs.Users;
using DriveMate.Application.Interfaces.IRepositories;
using DriveMate.Application.Interfaces.Services;

namespace DriveMate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileResponse?> GetProfileAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return null;
        }

        return MapToProfileResponse(user);
    }

    public async Task<UserProfileResponse?> UpdateProfileAsync(
        Guid userId,
        UpdateUserProfileRequest request)
    {
        var firstName = request.FirstName.Trim();
        var lastName = request.LastName.Trim();
        var phoneNumber = request.PhoneNumber?.Trim();

        var user = await _userRepository.UpdateProfileAsync(
            userId,
            firstName,
            lastName,
            phoneNumber);

        if (user is null)
        {
            return null;
        }

        return MapToProfileResponse(user);
    }

    private static UserProfileResponse MapToProfileResponse(
        Domain.Models.Users.User user)
    {
        return new UserProfileResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}