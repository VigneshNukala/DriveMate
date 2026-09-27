using System.Data;

namespace DriveMate.Application.Interfaces.IDatabase;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}