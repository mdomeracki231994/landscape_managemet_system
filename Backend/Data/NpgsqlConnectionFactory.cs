using System.Data;
using Npgsql;

namespace Backend.Data;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var conn = new NpgsqlConnection(_connectionString);
        return conn;
    }
}

