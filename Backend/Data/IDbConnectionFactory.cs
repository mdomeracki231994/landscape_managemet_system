using System.Data;

namespace Backend.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

