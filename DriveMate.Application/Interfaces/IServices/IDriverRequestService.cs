using DriveMate.Application.DTOs.DriverRequests;

namespace DriveMate.Application.Interfaces.Services;

public interface IDriverRequestService
{
    Task<DriverRequestResponse> CreateAsync(Guid customerId, CreateDriverRequestRequest request);

    Task<DriverRequestResponse?> GetByIdAsync(Guid customerId, Guid requestId);

    Task<IReadOnlyList<DriverRequestResponse>> GetMyRequestsAsync(Guid customerId);

    Task<IReadOnlyList<DriverRequestResponse>> GetPendingRequestsAsync();

    Task<bool> AcceptAsync(Guid driverId, Guid requestId);

    Task<bool> RejectAsync(Guid driverId, Guid requestId);
}