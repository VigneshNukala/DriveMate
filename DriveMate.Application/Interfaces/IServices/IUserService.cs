using DriveMate.Application.DTOs.Users;

namespace DriveMate.Application.Interfaces.Services;

public interface IUserService
{
    Task<UserProfileResponse?> GetProfileAsync(Guid userId);

    Task<UserProfileResponse?> UpdateProfileAsync(Guid userId, UpdateUserProfileRequest request);
}