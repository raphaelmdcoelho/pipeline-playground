using Dapper;
using PipelinePlayground.Infrastructure.Data.Factories;
using System.Data;

namespace PipelinePlayground.Infrastructure.Data.Contexts
{
    public class OdbcContext : IOdbcContext
    {
        private readonly IOdbcConnectionFactory _odbcConnectionFactory;
        private IDbConnection? _dbConnection;

        public OdbcContext(
            IOdbcConnectionFactory odbcConnectionFactory)
        {
            _odbcConnectionFactory = odbcConnectionFactory;
        }

        public void OpenConnection(string connectionString)
        {
            if(_dbConnection is not null)
                throw new InvalidOperationException("Connection is already open.");

            _dbConnection = _odbcConnectionFactory.GetConnection(connectionString);
        }

        public async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(string query)
        {
            if (_dbConnection is null)
                throw new InvalidOperationException("Connection is not open.");

            return await _dbConnection!.QueryAsync<TResult>(query);
        }
    }
}
