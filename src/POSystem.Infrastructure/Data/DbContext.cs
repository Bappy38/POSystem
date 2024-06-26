using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using POSystem.Domain.Constants;
using System.Data;

namespace POSystem.Infrastructure.Data;

public class DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString(DbConstants.DefaultConnection);
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
