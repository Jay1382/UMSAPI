using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace FileManager.DataAccessLayer.DataContext
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;
        public DapperDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = _configuration.GetConnectionString("connection");            
        }

        public IDbConnection CreateConnection() { 
            return new SqlConnection(connectionstring);
        }
    }
}
