using DriveMate.Application.DTOs.DriverRequests;
using DriveMate.Application.Interfaces.IRepositories;
using DriveMate.Application.Interfaces.Services;
using DriveMate.Domain.Models.DriverRequests;

namespace DriveMate.Application.Services;

public class DriverRequestService : IDriverRequestService
{
    private readonly IDriverRequestRepository _repository;

    public DriverRequestService(IDriverRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<DriverRequestResponse> CreateAsync(Guid customerId, CreateDriverRequestRequest request)
    {
        var pickupLocation = request.PickupLocation.Trim();

        if (string.IsNullOrWhiteSpace(pickupLocation))
        {
            throw new InvalidOperationException(
                "Pickup location is required.");
        }

        if (request.DurationHours <= 0)
        {
            throw new InvalidOperationException(
                "Duration must be greater than zero.");
        }

        var driverRequest = new DriverRequest
        {
            Id = Guid.NewGuid(),

            CustomerId = customerId,

            PickupLocation = pickupLocation,

            DropLocation =
                string.IsNullOrWhiteSpace(request.DropLocation)
                    ? null
                    : request.DropLocation.Trim(),

            ServiceDate = request.ServiceDate,

            StartTime = request.StartTime,

            DurationHours = request.DurationHours,

            Notes =
                string.IsNullOrWhiteSpace(request.Notes)
                    ? null
                    : request.Notes.Trim(),

            Status = "Pending",

            CreatedAt = DateTime.UtcNow
        };

        var createdRequest =
            await _repository.CreateAsync(driverRequest);

        return MapToResponse(createdRequest);
    }

    public async Task<DriverRequestResponse?> GetByIdAsync(Guid customerId, Guid requestId)
    {
        var driverRequest =
            await _repository.GetByIdAsync(requestId);

        if (driverRequest is null ||
            driverRequest.CustomerId != customerId)
        {
            return null;
        }

        return MapToResponse(driverRequest);
    }

    public async Task<IReadOnlyList<DriverRequestResponse>> GetMyRequestsAsync(Guid customerId)
    {
        var requests =
            await _repository.GetByCustomerIdAsync(customerId);

        return requests.Select(MapToResponse).ToList();
    }

    private static DriverRequestResponse MapToResponse(DriverRequest request)
    {
        return new DriverRequestResponse
        {
            Id = request.Id,
            CustomerId = request.CustomerId,
            DriverId = request.DriverId,
            PickupLocation = request.PickupLocation,
            DropLocation = request.DropLocation,
            ServiceDate = request.ServiceDate,
            StartTime = request.StartTime,
            DurationHours = request.DurationHours,
            Notes = request.Notes,
            Status = request.Status,
            CreatedAt = request.CreatedAt,
            ModifiedAt = request.ModifiedAt
        };
    }

    public async Task<IReadOnlyList<DriverRequestResponse>> GetPendingRequestsAsync()
    {
        var requests = await _repository.GetPendingRequestsAsync();
        return requests.Select(MapToResponse).ToList();
    }

    public async Task<bool> AcceptAsync(Guid driverId, Guid requestId)
    {
        return await _repository.UpdateStatusAsync(requestId, driverId, "Accepted");
    }

    public async Task<bool> RejectAsync(Guid driverId, Guid requestId)
    {
        return await _repository.UpdateStatusAsync(requestId, driverId, "Rejected");
    }
}