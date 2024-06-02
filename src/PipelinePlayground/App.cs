
using Dapper;
using PipelinePlayground.Configurations;
using PipelinePlayground.Entities;
using PipelinePlayground.Infrastructure.Data.Factories;

namespace PipelinePlayground
{
    public sealed class App : IApp
    {
        private readonly IOdbcConnectionFactory _odbcConnectionFactory;
        private readonly string _connectionString;

        public App(
            IOdbcConnectionFactory odbcConnectionFactory,
            Func<OdbcConfiguration> odbcConfiguration)
        {
            _odbcConnectionFactory = odbcConnectionFactory;
            _connectionString = odbcConfiguration().ConnectionString; // call the function
        }

        public async Task Run()
        {
            using var connection = _odbcConnectionFactory.GetConnection(_connectionString);

            var sql  = "SELECT * FROM app_user";

            var users = await connection.QueryAsync<AppUser>(sql);

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
