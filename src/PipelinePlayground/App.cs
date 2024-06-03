using PipelinePlayground.Configurations;
using PipelinePlayground.Entities;
using PipelinePlayground.Infrastructure.Data.Contexts;

namespace PipelinePlayground
{
    public sealed class App : IApp
    {
        private readonly IOdbcContext _odbcContext;
        private string _connectionString;

        public App(
            IOdbcContext odbcContext,
            Func<OdbcConfiguration> odbcConfiguration)
        {
            _odbcContext = odbcContext;
            _connectionString = odbcConfiguration().ConnectionString; // call the function
        }

        public async Task Run()
        {
            _odbcContext.OpenConnection(_connectionString);

            var sql  = "SELECT * FROM app_user";

            var users = await _odbcContext.ExecuteAsync<AppUser>(sql);

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
