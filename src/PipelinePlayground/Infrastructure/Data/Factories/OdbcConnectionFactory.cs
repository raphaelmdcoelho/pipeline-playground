using System.Data;
using System.Data.Odbc;

namespace PipelinePlayground.Infrastructure.Data.Factories
{
    public class OdbcConnectionFactory : IOdbcConnectionFactory
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new OdbcConnection(connectionString);
        }
    }
}
