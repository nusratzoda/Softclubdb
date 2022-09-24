namespace Domain.DataContext;
using Npgsql;
using Microsoft.Extensions.Configuration;
public class DataContext
{
    private readonly IConfiguration _configuration;
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("SqlConnection");
        return new NpgsqlConnection(connectionString);
    }
}
