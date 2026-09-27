using DriveMate.Domain.Models.Users;

namespace DriveMate.Application.Interfaces.IRepositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User> CreateAsync(User user);
}