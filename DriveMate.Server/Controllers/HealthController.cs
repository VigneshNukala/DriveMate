using Microsoft.AspNetCore.Mvc;

namespace DriverServicePlatform.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "OK",
            message = "Driver Service Platform API is running"
        });
    }
}