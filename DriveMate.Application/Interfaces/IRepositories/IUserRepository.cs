using DriveMate.Domain.Models.Users;

namespace DriveMate.Application.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(Guid id);

    Task<User> CreateAsync(User user);
    Task<User?> UpdateProfileAsync(
        Guid id,
        string firstName,
        string lastName,
        string? phoneNumber);
}