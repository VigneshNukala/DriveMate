using Dapper;
using DriveMate.Domain.Models.Users;
using DriveMate.Application.Interfaces.IDatabase;
using DriveMate.Application.Interfaces.IRepositories;

namespace DriveMate.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = """
            SELECT
                id,
                first_name AS FirstName,
                last_name AS LastName,
                email,
                phone_number AS PhoneNumber,
                password_hash AS PasswordHash,
                role,
                is_active AS IsActive,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                modified_at AS ModifiedAt,
                modified_by AS ModifiedBy
            FROM users
            WHERE email = @Email
            LIMIT 1;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<User>(
            sql,
            new { Email = email });
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string sql = """
            SELECT
                id,
                first_name AS FirstName,
                last_name AS LastName,
                email,
                phone_number AS PhoneNumber,
                password_hash AS PasswordHash,
                role,
                is_active AS IsActive,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                modified_at AS ModifiedAt,
                modified_by AS ModifiedBy
            FROM users
            WHERE id = @Id
            LIMIT 1;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<User>(
            sql,
            new { Id = id });
    }

    public async Task<User> CreateAsync(User user)
    {
        const string sql = """
            INSERT INTO users
            (
                id,
                first_name,
                last_name,
                email,
                phone_number,
                password_hash,
                role,
                is_active,
                created_at
            )
            VALUES
            (
                @Id,
                @FirstName,
                @LastName,
                @Email,
                @PhoneNumber,
                @PasswordHash,
                @Role,
                @IsActive,
                @CreatedAt
            )
            RETURNING
                id,
                first_name AS FirstName,
                last_name AS LastName,
                email,
                phone_number AS PhoneNumber,
                password_hash AS PasswordHash,
                role,
                is_active AS IsActive,
                created_at AS CreatedAt;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleAsync<User>(
            sql,
            user);
    }

    public async Task<User?> UpdateProfileAsync(Guid id, string firstName,    string lastName, string? phoneNumber)
    {
        const string sql = """
            UPDATE users
            SET
                first_name = @FirstName,
                last_name = @LastName,
                phone_number = @PhoneNumber,
                modified_at = NOW()
            WHERE id = @Id
            RETURNING
                id,
                first_name AS FirstName,
                last_name AS LastName,
                email,
                phone_number AS PhoneNumber,
                password_hash AS PasswordHash,
                role,
                is_active AS IsActive,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                modified_at AS ModifiedAt,
                modified_by AS ModifiedBy;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<User>(
            sql,
            new
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            });
    }
}
