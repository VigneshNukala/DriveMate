using Dapper;
using DriveMate.Application.Interfaces.IRepositories;
using DriveMate.Domain.Models.DriverRequests;
using DriveMate.Application.Interfaces.IDatabase;

namespace DriveMate.Application.Repositories;

public class DriverRequestRepository : IDriverRequestRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DriverRequestRepository(
        IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<DriverRequest> CreateAsync(DriverRequest driverRequest)
    {
        const string sql = """
            INSERT INTO driver_requests
            (
                id,
                customer_id,
                driver_id,
                pickup_location,
                drop_location,
                service_date,
                start_time,
                duration_hours,
                notes,
                status,
                created_at,
                modified_at
            )
            VALUES
            (
                @Id,
                @CustomerId,
                @DriverId,
                @PickupLocation,
                @DropLocation,
                @ServiceDate,
                @StartTime,
                @DurationHours,
                @Notes,
                @Status,
                @CreatedAt,
                @ModifiedAt
            )
            RETURNING
                id,
                customer_id AS CustomerId,
                driver_id AS DriverId,
                pickup_location AS PickupLocation,
                drop_location AS DropLocation,
                service_date AS ServiceDate,
                start_time AS StartTime,
                duration_hours AS DurationHours,
                notes AS Notes,
                status AS Status,
                created_at AS CreatedAt,
                modified_at AS ModifiedAt;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleAsync<DriverRequest>(sql, driverRequest);
    }

    public async Task<DriverRequest?> GetByIdAsync(Guid id)
    {
        const string sql = """
            SELECT
                id,
                customer_id AS CustomerId,
                driver_id AS DriverId,
                pickup_location AS PickupLocation,
                drop_location AS DropLocation,
                service_date AS ServiceDate,
                start_time AS StartTime,
                duration_hours AS DurationHours,
                notes AS Notes,
                status AS Status,
                created_at AS CreatedAt,
                modified_at AS ModifiedAt
            FROM driver_requests
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<DriverRequest>(sql, new { Id = id });
    }

    public async Task<IReadOnlyList<DriverRequest>> GetByCustomerIdAsync(Guid customerId)
    {
        const string sql = """
            SELECT
                id,
                customer_id AS CustomerId,
                driver_id AS DriverId,
                pickup_location AS PickupLocation,
                drop_location AS DropLocation,
                service_date AS ServiceDate,
                start_time AS StartTime,
                duration_hours AS DurationHours,
                notes AS Notes,
                status AS Status,
                created_at AS CreatedAt,
                modified_at AS ModifiedAt
            FROM driver_requests
            WHERE customer_id = @CustomerId
            ORDER BY created_at DESC;
            """;

        using var connection =_connectionFactory.CreateConnection();

        var requests =await connection.QueryAsync<DriverRequest>(sql,new { CustomerId = customerId });

        return requests.ToList();
    }
    
    public async Task<IReadOnlyList<DriverRequest>> GetPendingRequestsAsync()
    {
        const string sql = """
            SELECT
                id,
                customer_id AS CustomerId,
                driver_id AS DriverId,
                pickup_location AS PickupLocation,
                drop_location AS DropLocation,
                service_date AS ServiceDate,
                start_time AS StartTime,
                duration_hours AS DurationHours,
                notes AS Notes,
                status AS Status,
                created_at AS CreatedAt,
                modified_at AS ModifiedAt
            FROM driver_requests
            WHERE status = 'Pending'
            ORDER BY created_at ASC;
            """;

        using var connection = _connectionFactory.CreateConnection();

        var requests = await connection.QueryAsync<DriverRequest>(sql);

        return requests.ToList();
    }

    public async Task<bool> UpdateStatusAsync(Guid requestId, Guid driverId, string status)
    {
        const string sql = """
            UPDATE driver_requests
            SET
                driver_id = @DriverId,
                status = @Status,
                modified_at = NOW()
            WHERE
                id = @RequestId
                AND status = 'Pending';
            """;

        using var connection =
            _connectionFactory.CreateConnection();

        var rowsAffected =
            await connection.ExecuteAsync(
                sql,
                new
                {
                    RequestId = requestId,
                    DriverId = driverId,
                    Status = status
                });

        return rowsAffected > 0;
    }
}