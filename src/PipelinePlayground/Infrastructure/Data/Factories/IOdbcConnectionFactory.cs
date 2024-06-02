using System.Data;

namespace PipelinePlayground.Infrastructure.Data.Factories
{
    public interface IOdbcConnectionFactory
    {
        public IDbConnection GetConnection(string connectionString);
    }
}
