using System.Security.Claims;

using DriveMate.Application.DTOs.DriverRequests;
using DriveMate.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Server.Controllers;

[ApiController]
[Route("api/driver-requests")]
[Authorize(Roles = "Customer")]
public class DriverRequestsController : ControllerBase
{
    private readonly IDriverRequestService _driverRequestService;

    public DriverRequestsController(
        IDriverRequestService driverRequestService)
    {
        _driverRequestService = driverRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDriverRequestRequest request)
    {
        var customerId = GetCurrentUserId();

        var result =
            await _driverRequestService.CreateAsync(
                customerId,
                request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyRequests()
    {
        var customerId = GetCurrentUserId();

        var result =
            await _driverRequestService.GetMyRequestsAsync(
                customerId);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id)
    {
        var customerId = GetCurrentUserId();

        var result =
            await _driverRequestService.GetByIdAsync(
                customerId,
                id);

        if (result is null)
        {
            return NotFound(new
            {
                message = "Driver request not found."
            });
        }

        return Ok(result);
    }

    private Guid GetCurrentUserId()
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out var customerId))
        {
            throw new UnauthorizedAccessException(
                "Invalid user identity.");
        }

        return customerId;
    }
}