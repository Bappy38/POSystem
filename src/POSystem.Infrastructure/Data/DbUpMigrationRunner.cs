using Dapper;
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using POSystem.Domain.Constants;

namespace POSystem.Infrastructure.Data;

public class DbUpMigrationRunner
{
    public static void MigrateDatabase(IConfiguration configuration)
    {
        var masterConnectionString = configuration.GetConnectionString(DbConstants.MasterConnection);
        var connectionString = configuration.GetConnectionString(DbConstants.DefaultConnection);
        var databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

        EnsureDatabaseExists(masterConnectionString, databaseName);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DbContext).Assembly)
            .WithVariable("GO", "GO")
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine($"Database Migration Failed");
            throw new Exception("Database Migration Failed");
        }
    }

    private static void EnsureDatabaseExists(string masterConnectionString, string databaseName)
    {
        using (var connection = new SqlConnection(masterConnectionString))
        {
            connection.Open();

            var databaseExists = connection.ExecuteScalar<bool>(
                $"SELECT CASE WHEN EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");

            if (!databaseExists)
            {
                connection.Execute($"CREATE DATABASE [{databaseName}]");
                Console.WriteLine($"Database '{databaseName}' created.");
            }
            else
            {
                Console.WriteLine($"Database '{databaseName}' already exists.");
            }
        }
    }
}
