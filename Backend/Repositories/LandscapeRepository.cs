using System.Data;
using Dapper;
using Backend.Data;
using Backend.Models;

namespace Backend.Repositories;

public class LandscapeRepository : ILandscapeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public LandscapeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Landscape>> GetAllAsync(CancellationToken ct = default)
    {
        const string sql = "SELECT id, name, description, created_at AS CreatedAt FROM landscapes ORDER BY id";
        await using var conn = (System.Data.Common.DbConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);
        var items = await conn.QueryAsync<Landscape>(new CommandDefinition(sql, cancellationToken: ct));
        return items;
    }

    public async Task<Landscape?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        const string sql = "SELECT id, name, description, created_at AS CreatedAt FROM landscapes WHERE id = @id";
        await using var conn = (System.Data.Common.DbConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);
        return await conn.QuerySingleOrDefaultAsync<Landscape>(new CommandDefinition(sql, new { id }, cancellationToken: ct));
    }

    public async Task<long> CreateAsync(Landscape landscape, CancellationToken ct = default)
    {
        const string sql = @"INSERT INTO landscapes (name, description) VALUES (@Name, @Description) RETURNING id";
        await using var conn = (System.Data.Common.DbConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);
        var id = await conn.ExecuteScalarAsync<long>(new CommandDefinition(sql, new { landscape.Name, landscape.Description }, cancellationToken: ct));
        return id;
    }

    public async Task<bool> UpdateAsync(Landscape landscape, CancellationToken ct = default)
    {
        const string sql = @"UPDATE landscapes SET name = @Name, description = @Description WHERE id = @Id";
        await using var conn = (System.Data.Common.DbConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, new { landscape.Id, landscape.Name, landscape.Description }, cancellationToken: ct));
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
        const string sql = "DELETE FROM landscapes WHERE id = @id";
        await using var conn = (System.Data.Common.DbConnection)_connectionFactory.CreateConnection();
        await conn.OpenAsync(ct);
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, new { id }, cancellationToken: ct));
        return rows > 0;
    }
}

