using System.Threading.Tasks;
using DriveMate.Application.DTOs.Authentication ;

namespace DriveMate.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    Task<AuthResponse> LoginAsync(LoginRequest request);
}