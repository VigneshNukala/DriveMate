using DriveMate.Domain.Models.Users;

namespace DriveMate.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}