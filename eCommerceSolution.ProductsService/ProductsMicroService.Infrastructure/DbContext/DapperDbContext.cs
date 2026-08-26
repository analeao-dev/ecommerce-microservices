using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ProductsMicroService.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IDbConnection _connection;
    
    public DapperDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        _connection = new NpgsqlConnection(connectionString);
    }
    
    public IDbConnection DbConnection => _connection;
}