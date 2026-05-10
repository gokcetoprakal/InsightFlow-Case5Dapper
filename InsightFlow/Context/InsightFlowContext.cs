using Microsoft.Data.SqlClient;
using System.Data;

namespace InsightFlow.Context
{
    public class InsightFlowContext
    {
        public readonly IConfiguration _configuration;
        public readonly string _connectionString;

        public InsightFlowContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
