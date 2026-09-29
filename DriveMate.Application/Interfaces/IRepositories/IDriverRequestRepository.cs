using DriveMate.Domain.Models.DriverRequests;

namespace DriveMate.Application.Interfaces.IRepositories;

public interface IDriverRequestRepository
{
    Task<DriverRequest> CreateAsync(DriverRequest driverRequest);

    Task<DriverRequest?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<DriverRequest>> GetByCustomerIdAsync(Guid customerId);

    Task<IReadOnlyList<DriverRequest>> GetPendingRequestsAsync();

    Task<bool> UpdateStatusAsync(Guid requestId, Guid driverId, string status);
}