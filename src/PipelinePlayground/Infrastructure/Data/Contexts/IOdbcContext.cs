namespace PipelinePlayground.Infrastructure.Data.Contexts
{
    public interface IOdbcContext
    {
        void OpenConnection(string connectionString);
        public Task<IEnumerable<TResult>> ExecuteAsync<TResult>(string query);
    }
}
