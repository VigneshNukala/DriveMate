using System.Security.Claims;

using DriveMate.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Server.Controllers;

[ApiController]
[Route("api/driver/requests")]
[Authorize(Roles = "Driver")]
public class DriverRequestsDriverController : ControllerBase
{
    private readonly IDriverRequestService _driverRequestService;

    public DriverRequestsDriverController(IDriverRequestService driverRequestService)
    {
        _driverRequestService = driverRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPendingRequests()
    {
        var requests = await _driverRequestService.GetPendingRequestsAsync();

        return Ok(requests);
    }

    [HttpPost("{id:guid}/accept")]
    public async Task<IActionResult> Accept(Guid id)
    {
        var driverId = GetCurrentUserId();

        var success =await _driverRequestService.AcceptAsync(driverId,id);

        if (!success)
        {
            return Conflict(new
            {
                message = "This request is no longer available."
            });
        }

        return Ok(new
        {
            message = "Driver request accepted successfully."
        });
    }

    [HttpPost("{id:guid}/reject")]
    public async Task<IActionResult> Reject(
        Guid id)
    {
        var driverId = GetCurrentUserId();

        var success =await _driverRequestService.RejectAsync(driverId, id);

        if (!success)
        {
            return Conflict(new
            {
                message = "This request is no longer available."
            });
        }

        return Ok(new
        {
            message = "Driver request rejected successfully."
        });
    }

    private Guid GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out var driverId))
        {
            throw new UnauthorizedAccessException("Invalid user identity.");
        }

        return driverId;
    }
}