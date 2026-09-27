using Dapper;
using DriveMate.Application.Interfaces.IDatabase;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Server.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly IDbConnectionFactory _connectionFactory;

    public HealthController(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    [HttpGet("database")]
    public async Task<IActionResult> Database()
    {
        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.ExecuteScalarAsync<int>(
            "SELECT 1;");

        return Ok(new
        {
            status = "Database connected",
            result
        });
    }
}